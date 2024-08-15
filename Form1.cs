using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

//TODO: Fix filtering system on the top right.
//TODO: Fix toggle button

namespace FileManager
{
    public partial class Form1 : Form
    {
        private TextWriterTraceListener logListener;                // Initialize logs
        private List<string> recentFolders = new List<string>();    // contains list of recent folders for ComboxBox
        private List<string> originFiles = new List<string>();      // contains list of all files in current Origin path
        private List<string> DestFiles = new List<string>();        // contains list of all files in current Dest path
        private string currentFilter = "All Files";                 // Variable to store the current filter
        private bool toggle = false;                                // determines state of ToggleButton
        private string originPath = "";
        private string destPath = "";

        public Form1()
        {
            InitializeComponent();
            MoveSelectedButton.Enabled = false;  // move selected button
            OriginFoldersListBox.SelectionMode = SelectionMode.MultiExtended;  // allows multiple items to be selected
            // enable drag and drop
            OriginFoldersListBox.AllowDrop = true;
            OriginFoldersListBox.DragEnter += OriginFolderListBoxDragEnter;
            OriginFoldersListBox.DragDrop += OriginFolderListBoxDragDrop;

            DestFoldersListBox.AllowDrop = true;
            DestFoldersListBox.DragEnter += DestFolderListBoxDragEnter;
            DestFoldersListBox.DragDrop += DestFolderListBoxDragDrop;

            // Load filter options from ExtensionFilter.txt
            string[] filterOptionLines = File.ReadAllLines("C:\\Users\\mbutk\\Documents\\Python Scripts\\FileManager\\ExtensionFilter.txt");
            FilterComboBox.Items.Add("All Files");
            foreach (string line in filterOptionLines)
            {
                int separatorIndex = line.IndexOf('|');
                if (separatorIndex != -1)
                {
                    string description = line.Substring(0, separatorIndex);
                    FilterComboBox.Items.Add(description);
                }
            }

            // Initialize the status strip control
            StatusStrip statusStrip1 = new StatusStrip();
            Controls.Add(statusStrip1); // Add the control to the form's controls collection
            statusStrip1.Items.Add("Ready");

            // Initialize the log listener
            string logFilePath = @"C:\Users\mbutk\Documents\Python Scripts\Diagnostics";
            logListener = new TextWriterTraceListener(logFilePath + "log.txt");
            Trace.Listeners.Add(logListener);

            // update recentFolders with all the files from previous sessions
            if (File.Exists("C:\\Users\\mbutk\\Documents\\Python Scripts\\FileManager\\RecentFolders.txt"))
            {
                recentFolders = File.ReadAllLines("C:\\Users\\mbutk\\Documents\\Python Scripts\\FileManager\\RecentFolders.txt").ToList();
                UpdateRecentFoldersComboBox();
            }
        }

        // when you press the toggle button, changes toggle bool value
        private void TogglePath_Click(object sender, EventArgs e)
        {
            toggle = !toggle;
        }

        // Was having trouble remembering how to get the directory every time so I made this method
        // simply returns the directory path if you put a file in it 
        private string GetDirectoryPath(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        // overloaded version to take an array of strings. This way I can always call this method no matter file format
        private string GetDirectoryPath(string[] filePaths)
        {
            if (filePaths != null && filePaths.Length > 0)
            {
                string firstFilePath = filePaths[0];
                string directoryPath = Path.GetDirectoryName(firstFilePath);
                return directoryPath;
            }

            return null; // Return null or an appropriate value in case of no files
        }

        // Gets the file name when the filePath is passed
        private string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        // returns a list of all the file paths in the directory passed
        private List<string> GetAllItemsInDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                string[] allItems = Directory.GetFileSystemEntries(directoryPath);
                return new List<string>(allItems);
            }
            else
            {
                // Handle the case when the directory doesn't exist
                return new List<string>();
            }
        }


        // allows users to drag folders/files onto origin ListBox
        private void OriginFolderListBoxDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                bool isAllDirectories = files.All(path => Directory.Exists(path));
                bool isAllFiles = files.All(path => File.Exists(path));

                if (isAllDirectories || isAllFiles)
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        // allows users to drop folders/files onto origin ListBox
        private void OriginFolderListBoxDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // get the files from the filedrop, then directory path
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                originPath = GetDirectoryPath(files);
                originFiles = GetAllItemsInDirectory(originPath);

                // Clear the existing items before adding new ones
                OriginFoldersListBox.Items.Clear();
                OriginFoldersListBox.Items.AddRange(originFiles.ToArray());

                // Update the originLabel with the selected folder's path
                originLabel.Text = originPath;
            }
            // check to see if move selected should be enabled
            MoveSelectedButton.Enabled = CanEnableMoveButton();
        }

        // update information when item is selected in ComboBox
        private void OriginComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFolderName = originFoldersComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedFolderName))
            {
                // Find the corresponding folder path in the recentFolders list using LINQ 
                string fullPath = recentFolders.FirstOrDefault(folder => Path.GetFileName(folder) == selectedFolderName);
                if (!string.IsNullOrEmpty(fullPath))
                {
                    originPath = fullPath; // Update the originPath with the correct path
                    originFiles = GetAllItemsInDirectory(originPath);

                    // Clear the existing items before adding new ones
                    OriginFoldersListBox.Items.Clear();
                    OriginFoldersListBox.Items.AddRange(originFiles.ToArray());
                    originLabel.Text = originPath;
                }
            }

            // check to see if move selected should be enabled
            MoveSelectedButton.Enabled = CanEnableMoveButton();
        }

        // Method to select the origin folder
        private void SelectOriginFolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog folderChooser = new FolderBrowserDialog())
                {
                    if (folderChooser.ShowDialog() == DialogResult.OK)
                    {
                        string originFolderPath = folderChooser.SelectedPath;
                        string[] entries = Directory.GetFileSystemEntries(originFolderPath);
                        OriginFoldersListBox.Items.Clear();
                        OriginFoldersListBox.Items.AddRange(entries);

                        // check to see if it already exists in recentFolders using LINQ and add if it doesn't exist
                        if (!recentFolders.Any(folder => folder == originFolderPath))
                        {
                            recentFolders.Insert(0, originFolderPath);
                        }
                        UpdateRecentFoldersComboBox();
                        originLabel.Text = originFolderPath;

                        // TODO: Was having problems implementing statusstrip. Can't figure out how to remove it so messages stack
                        //SetStatusMessage("Origin folder selected: " + originFolderPath);
                        Trace.WriteLine("Origin folder selected: " + originFolderPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while selecting the origin folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Trace.WriteLine("Error selecting origin folder: " + ex.Message);
                SetStatusMessage("Error selecting origin folder.");
            }

            // check to see if move selected should be enabled
            MoveSelectedButton.Enabled = CanEnableMoveButton();
        }

        // allows users to start a drag on folders from your system
        // I opted not to allow file drop because you can't move a file into a file
        private void DestFolderListBoxDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        // allows users to drop folders/file onto destination ListBox
        private void DestFolderListBoxDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length == 1 && Directory.Exists(files[0]))
                {
                    string destFolderPath = files[0];
                    string[] entries = Directory.GetFileSystemEntries(destFolderPath);
                    DestFoldersListBox.Items.Clear(); // Clear existing items before adding new ones
                    DestFoldersListBox.Items.AddRange(entries);
                    destLabel.Text = destFolderPath;
                }
            }

            // check to see if move selected should be enabled
            MoveSelectedButton.Enabled = CanEnableMoveButton();
        }

        // update information when item is selected in the file selector combobox for dest
        private void DestinationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFolderName = destFoldersComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedFolderName))
            {
                // Find the corresponding full path in the recentFolders list using LINQ 
                string fullPath = recentFolders.FirstOrDefault(folder => Path.GetFileName(folder) == selectedFolderName);

                if (!string.IsNullOrEmpty(fullPath))
                {
                    // Update the OriginFolderListBox with the selected folder's content
                    string[] entries = Directory.GetFileSystemEntries(fullPath);
                    DestFoldersListBox.Items.Clear();
                    DestFoldersListBox.Items.AddRange(entries);

                    destLabel.Text = fullPath; // Update label to display selected folder
                                               // Update other controls as needed (e.g., listbox)
                }
            }

            // check to see if move selected should be enabled
            MoveSelectedButton.Enabled = CanEnableMoveButton();
        }

        private void SelectDestinationFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderChooser = new FolderBrowserDialog())
            {
                if (folderChooser.ShowDialog() == DialogResult.OK)
                {
                    string destFolderPath = folderChooser.SelectedPath;
                    string[] entries = Directory.GetFileSystemEntries(destFolderPath);
                    DestFoldersListBox.Items.Clear(); // Clear existing items before adding new ones
                    DestFoldersListBox.Items.AddRange(entries);
                    // updates recent folders ComboBox
                    // check to see if it already exists in recentFolders using LINQ and add if it doesn't exist
                    if (!recentFolders.Any(folder => folder == destFolderPath))
                    {
                        recentFolders.Insert(0, destFolderPath);
                    }
                    UpdateRecentFoldersComboBox();
                    destLabel.Text = destFolderPath;
                }
            }
            // check to see if move selected should be enabled
            MoveSelectedButton.Enabled = CanEnableMoveButton();
        }
  
        private void MoveSelectedButton_Click(object sender, EventArgs e)
        {
            string originFolderPath = originLabel.Text;
            string destinationFolderPath = destLabel.Text;

            if (originFolderPath == destinationFolderPath)
            {
                MessageBox.Show("You have selected the same file path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedItems = OriginFoldersListBox.SelectedItems.Cast<string>().ToList();
            bool isMovingFolder = false; // Flag to track if any folders are being moved

            if (selectedItems.Count == 0)
            {
                // Check if any folders are being moved
                foreach (var selectedItem in OriginFoldersListBox.Items)
                {
                    string sourcePath = selectedItem.ToString();
                    if (Directory.Exists(sourcePath))
                    {
                        isMovingFolder = true; // At least one folder is being moved
                        break; // No need to check further
                    }
                }

                if (isMovingFolder)
                {
                    string message = "You are about to move one or more folders. Do you want to proceed?";
                    DialogResult result = MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                        return;
                }

                // Prompt user if they are moving all files when nothing is selected
                string moveAllFilesMessage = "Are you sure you want to move all items in the folder?";
                DialogResult moveAllFilesResult = MessageBox.Show(moveAllFilesMessage, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (moveAllFilesResult == DialogResult.Yes)
                {
                    if (OriginFoldersListBox.Items.Count == 0)
                    {
                        MessageBox.Show("No items selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    foreach (var selectedItem in OriginFoldersListBox.Items)
                    {
                        string sourcePath = selectedItem.ToString();
                        string destinationPath = Path.Combine(destinationFolderPath, Path.GetFileName(sourcePath));

                        if (File.Exists(sourcePath))
                        {
                            File.Move(sourcePath, destinationPath);
                        }
                        else if (Directory.Exists(sourcePath))
                        {
                            if (CanMoveFolder(sourcePath))
                            {
                                Directory.Move(sourcePath, destinationPath);
                            }
                        }
                    }

                    RefreshListBoxes();
                }
            }
            else
            {
                // Handle the case when selected items exist
                foreach (var selectedItem in selectedItems)
                {
                    string sourcePath = selectedItem;
                    if (Directory.Exists(sourcePath))
                    {
                        isMovingFolder = true; // At least one folder is being moved
                        break; // No need to check further
                    }
                }

                if (isMovingFolder)
                {
                    string message = "You are about to move one or more folders. Do you want to proceed?";
                    DialogResult result = MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                        return;
                }

                foreach (var selectedItem in selectedItems)
                {
                    string sourcePath = selectedItem;
                    string destinationPath = Path.Combine(destinationFolderPath, Path.GetFileName(sourcePath));

                    if (File.Exists(sourcePath))
                    {
                        File.Move(sourcePath, destinationPath);
                    }
                    else if (Directory.Exists(sourcePath))
                    {
                        if (CanMoveFolder(sourcePath))
                        {
                            Directory.Move(sourcePath, destinationPath);
                        }
                    }
                }

                RefreshListBoxes();
            }
        }

        // this method makes sure there is a valid path before allowing files to be moved
        private bool CanEnableMoveButton()
        {
            string originPath = originLabel.Text;
            string destPath = destLabel.Text;

            // makes sure this is a valid path
            return !string.IsNullOrEmpty(originPath) &&
                   !string.IsNullOrEmpty(destPath) &&
                   Directory.Exists(originPath) &&
                   Directory.Exists(destPath) &&
                   OriginFoldersListBox.Items.Count > 0;
        }

        private void RefreshListBoxes()
        {
            string[] originEntries = Directory.GetFileSystemEntries(originLabel.Text);
            OriginFoldersListBox.Items.Clear(); // Clear existing items before adding new ones
            OriginFoldersListBox.Items.AddRange(originEntries);

            string[] destEntries = Directory.GetFileSystemEntries(destLabel.Text);
            DestFoldersListBox.Items.Clear(); // Clear existing items before adding new ones
            DestFoldersListBox.Items.AddRange(destEntries);

            //StatusStrip statusStrip1.Items[0].Text = "List refreshed.";

        }

        //Updates the ComboBox displaying recently used folders
        private void UpdateRecentFoldersComboBox()
        {
            originFoldersComboBox.Items.Clear();
            originFoldersComboBox.Items.AddRange(recentFolders.Select(folder => Path.GetFileName(folder)).ToArray());

            destFoldersComboBox.Items.Clear();
            destFoldersComboBox.Items.AddRange(recentFolders.Select(folder => Path.GetFileName(folder)).ToArray());
        }

        // TODO: Incorporate way to filter combo box. I can populate it but I need to make sure it interacts with rest of program
        // I believe best way to do this will be to extract the extension in this method. Right now it's grabbing the whole text 
        // but I just want it to be like txt. 
        // Also, need to make it so that program doesn't crash when labels don't show file path at beginning of program 

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = FilterComboBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < FilterComboBox.Items.Count)
            {
                string selectedFilter = FilterComboBox.Items[selectedIndex].ToString();
                ApplyFilterToListBox(OriginFoldersListBox, selectedFilter);
                ApplyFilterToListBox(DestFoldersListBox, selectedFilter);
            }
        }

        private void ApplyFilterToListBox(ListBox listBox, string filter)
        {
            string originDirectory = originLabel.Text;
            string destDirectory = destLabel.Text;
            string[] allOriginFiles = Directory.GetFiles(originDirectory);
            string[] allDestFiles = Directory.GetFiles(destDirectory);

            if (filter == "All Files")
            {
                // Clear the listBox and add all files in directory
                listBox.Items.Clear();
                listBox.Items.AddRange(allOriginFiles);
            }
            else
            {
                string extension = filter.Substring(filter.LastIndexOf('(') + 1, filter.Length - filter.LastIndexOf('(') - 2);

                // Clear the listBox and add filtered files in directory
                listBox.Items.Clear();
                listBox.Items.AddRange(allOriginFiles
                    .Where(file => Path.GetExtension(file).Equals(extension, StringComparison.OrdinalIgnoreCase))
                    .ToArray());
            }
        }

        // TODO: Come back and status messages at the bottom of the program. Right now it only says "Ready"
        private void SetStatusMessage(string message)
        {
            StatusStrip statusStrip1 = new StatusStrip();
            Controls.Add(statusStrip1); // Add the control to the form's controls collection
            statusStrip1.Items.Add(message);
        }

        // this checks for special files that uses the Windows API. For example, Music contains the file desktop.ini even though
        // the folder is empty. That's a special file that gives it the custom appearance. I am choosing to disable moving folders
        // like that for now in case it causes system issues
        private bool IsDesktopIni(string path)
        {
            return string.Equals(Path.GetFileName(path), "desktop.ini", StringComparison.OrdinalIgnoreCase);
        }

        // check to see if a folder contains any special files that shouldn't be moved 
        private bool ContainsHiddenFiles(string folderPath)
        {
            string[] hiddenFileNames = { "desktop.ini", "Thumbs.db", ".DS_Store", "IconCache.db" };

            foreach (string fileName in hiddenFileNames)
            {
                string filePath = Path.Combine(folderPath, fileName);
                if (File.Exists(filePath) || Directory.Exists(filePath))
                {
                    return true;
                }
            }

            return false;
        }

        // method to check first if folder can be moved and display an error message if it contains a special hidden file
        private bool CanMoveFolder(string folderPath)
        {
            if (ContainsHiddenFiles(folderPath))
            {
                MessageBox.Show("The folder contains hidden files that may cause issues if moved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // update list of recent folders so program can populate list upon opening program
        private void SaveRecentFoldersToFile()
        {
            try
            {
                // Debug output to check if the method is called
                Debug.WriteLine("SaveRecentFoldersToFile method called");

                // only store the most recent 10 
                var limitedRecentFolders = recentFolders.Take(10);
                File.WriteAllLines("C:\\Users\\mbutk\\Documents\\Python Scripts\\FileManager\\RecentFolders.txt", limitedRecentFolders);
                Debug.WriteLine("Data written to file");

                // You can also check the content of the list
                foreach (string folder in recentFolders)
                {
                    Debug.WriteLine("Recent Folder: " + folder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving recent folders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // save recent folders to file upon closing program
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveRecentFoldersToFile();
        }
    }
}

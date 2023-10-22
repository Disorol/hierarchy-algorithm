using Avalonia.Controls;
using System.Collections.ObjectModel;
using System;
using System.Xml.Linq;
using System.IO;
using Avalonia.Controls.Shapes;

namespace treeTest
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Node> Nodes { get; set; }

        public MainWindow()
        {
            Nodes = new ObservableCollection<Node>() { new Node(@"C:\Users\T\Desktop\txts") };

            InitializeComponent();

            TreeExample.ItemsSource = Nodes;
        }

        public ObservableCollection<Node> GetSubfolders(Node nodeObject)
        {

            return new ObservableCollection<Node>();
        }
    }

    /*
        A class with a recursive overloaded constructor for traversing through all directories
        and creating a hierarchy of directories and files using ObservableCollection.
        !!! Files and directories are considered different concepts !!!
    */
    public class Node
    {
        // Collection of child files for this directory. Can be Null for storing files.
        public ObservableCollection<Node>? Subfiles { get; set; }

        // File or directory title. Maybe Null so that empty folders can be opened.
        public string? Title { get; set; }

        // The path to this file. Maybe Null so that empty folders can be opened.
        public string? PathString { get; set; }

        // Overloaded Constructor for the directory path.
        public Node(string path)
        {
            PathString = path;

            Title = System.IO.Path.GetFileName(path);

            Subfiles = new ObservableCollection<Node>();

            if (Directory.GetFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly).Length > 0)
            {
                if (Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly).Length > 0)
                {
                    foreach (string subpath in Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly))
                    {
                        Node node = new Node(subpath);

                        Subfiles.Add(node);
                    }
                }

                if (Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).Length > 0)
                {
                    foreach (string subpath in Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly))
                    {
                        Node node = new Node(subpath, true);

                        Subfiles.Add(node);
                    }
                }
            }
            else
            {
                // Creating an empty directory

                Node node = new Node(true);

                Subfiles.Add(node);
            }
        }

        // Overloaded Constructor for the file path.
        public Node(string path, bool isFile)
        {
            PathString = path;

            Title = System.IO.Path.GetFileName(path);
        }

        // Overloaded constructor for opening empty directories
        public Node(bool isEmpty) {}
    }
}
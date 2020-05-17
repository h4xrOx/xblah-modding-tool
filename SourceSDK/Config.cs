using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace source_modding_tool.SourceSDK
{
    public class Config
    {
        private List<KeyValue> children = null;
        private Dictionary<string, List<KeyValue>> childrenIndex = null;

        public Config()
        {
            children = new List<KeyValue>();
            childrenIndex = new Dictionary<string, List<KeyValue>>();
        }

        public static KeyValue readChunkfile(String path)
        {
            // Parse Valve chunkfile format
            KeyValue list = new KeyValue("root");

            if (File.Exists(path))
            {
                //try
                //{
                    using (StreamReader r = new StreamReader(path))
                    {
                        while (r.Peek() >= 0)
                        {
                            String line = r.ReadLine();
                            line = line.Trim();
                            line = Regex.Replace(line, @"\s+", " ");

                            // Ignore the commented part of the line
                            if (line.StartsWith("//"))
                                continue;

                            string[] words = splitByWords(line);

                        if (words.Length >= 1)
                        {
                            string key = words[0].Replace("\"", string.Empty).ToLower();
                            KeyValue value = new KeyValue(key, words[1]);
                            list.addChild(value);
                        }
                        }
                    }
                //}
                //catch (Exception e)
                //{
                   // Debugger.Break();
                  //  XtraMessageBox.Show("Could not read file \"" + path + "\". It's structure is broken.");
                   // return null;
                //}
            }
            else
            {
                XtraMessageBox.Show("Could not find file \"" + path + "\" to read.");
                return null;
            }

            return list;
        }

        public void addChild(KeyValue value)
        {
            if (!childrenIndex.ContainsKey(value.getKey()))
                childrenIndex.Add(value.getKey(), new List<KeyValue>());

            childrenIndex[value.getKey()].Add(value);
            children.Add(value);
        }

        public void addChild(string key, string value) { addChild(new KeyValue(key, value)); }

        public void clearChildren()
        {
            childrenIndex = new Dictionary<string, List<KeyValue>>();
            children = new List<KeyValue>();
        }

        public KeyValue getChildByKey(string key)
        {
            if (childrenIndex != null && childrenIndex.ContainsKey(key))
                return childrenIndex[key][0];

            return null;
        }

        public List<KeyValue> getChildren() { return children; }

        public Dictionary<string, List<KeyValue>> getChildrenByKey() { return childrenIndex; }

        public string getValue(string key)
        {
            KeyValue child = getChildByKey(key);
            if (child != null)
                return child.getValue();

            return string.Empty;
        }

        public void setValue(string key, string value)
        {
            if (childrenIndex == null)
                return;

            KeyValue child = getChildByKey(key);
            if (child != null)
            {
                child.setValue(value);
                return;
            }

            addChild(new KeyValue(key, value));
        }

        public static string[] splitByWords(string fullString)
        {
            List<string> words = new List<string>();

            string[] parts = fullString.Split('\"');
            for (int i = 0; i < parts.Length; i++)
            {
                if (i % 2 == 1)
                {
                    // between quotes
                    string subpart = parts[i].Replace("\"", string.Empty);
                    words.Add(subpart);
                }
                else
                {
                    string[] subparts = parts[i].Split(null);
                    // outside quotes
                    foreach (string subpart in subparts)
                    {
                        if (subpart != string.Empty && subpart != " ")
                            words.Add(subpart);
                    }
                }
            }
            return words.ToArray();
        }

        public static void writeChunkFile(string path, KeyValue root) { writeChunkFile(path, root, Encoding.UTF8); }

        public static void writeChunkFile(string path, KeyValue root, bool quotes)
        { writeChunkFile(path, root, quotes, Encoding.UTF8); }

        public static void writeChunkFile(string path, KeyValue root, Encoding encoding)
        { writeChunkFile(path, root, true, encoding); }

        public static void writeChunkFile(string path, KeyValue root, bool quotes, Encoding encoding)
        {
            List<string> lines = writeChunkFileTraverse(root, 0, quotes);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllLines(path, lines, encoding);
        }

        private static List<string> writeChunkFileTraverse(KeyValue root, int level, bool quotes)
        {
            List<string> lines = new List<string>();

            foreach(KeyValue node in root.getChildren())
                lines.Add(
                    (quotes ? "\"" : string.Empty) +
                    node.getKey() +
                    (quotes ? "\"" : string.Empty) +
                    "\t\"" +
                    node.getValue() +
                    "\"");

            return lines;
        }
    }
}

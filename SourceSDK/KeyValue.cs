using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace windows_source1ide.SourceSDK
{
    public class KeyValue
    {
        private string key = "";
        private string value = null;
        private Dictionary<string, List<KeyValue>> children = null;

        public KeyValue(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public KeyValue(string key)
        {
            this.key = key;
            children = new Dictionary<string, List<KeyValue>>();
        }

        public bool isParentKey()
        {
            return (children != null && value == null);
        }

        public void addChild(string key, KeyValue value)
        {
            if (!children.ContainsKey(key))
                children.Add(key, new List<KeyValue>());

            children[key].Add(value);
        }

        public void addChild(KeyValue value)
        {
            if (!children.ContainsKey(value.getKey()))
                children.Add(value.getKey(), new List<KeyValue>());

            children[value.getKey()].Add(value);
        }

        public KeyValue getChild(string key)
        {
            if (children != null && children.ContainsKey(key))
                return children[key][0];

            return null;
        }

        public List<KeyValue> getChildren(string key)
        {
            if (children != null && children.ContainsKey(key))
                return children[key];

            return null;
        }

        public Dictionary<string, List<KeyValue>> getChildren()
        {
            return children;
        }

        public List<KeyValue> getChildrenList()
        {
            List<KeyValue> result = new List<KeyValue>();
            foreach (List<KeyValue> list in children.Values)
            {
                result.AddRange(list);
            }
            return result;
        }

        public KeyValue findChild(string key)
        {
            if (children != null && children.ContainsKey(key))
                return children[key][0];

            if (children != null)
                foreach(string k in children.Keys)
                    foreach(KeyValue child in children[k])
                    {
                        KeyValue result = child.findChild(key);
                        if (result != null)
                            return result;
                    }

            return null;
        }

        public List<KeyValue> findChildren(string key)
        {
            List<KeyValue> result = new List<KeyValue>();
            if (children != null && children.ContainsKey(key))
                result.AddRange(children[key]);

            if (children != null)
                foreach (string k in children.Keys)
                    foreach (KeyValue child in children[k])
                    {
                        result.AddRange(child.findChildren(key));
                    }

            return result;
        }

        public void clearChildren()
        {
            children = new Dictionary<string, List<KeyValue>>();
        }

        public string getValue()
        {
            return value;
        }

        public string getValue(string key)
        {
            KeyValue child = getChild(key);
            if (child != null)
                return child.getValue();

            return "";
        }

        public string getKey()
        {
            return key;
        }

        public void setValue(string value)
        {
            if (this.value != null && children == null)
                this.value = value;
        }

        public void setValue(string key, string value)
        {
            if (this.value != null || children == null)
                return;

            KeyValue child = getChild(key);
            if (child != null)
            {
                child.setValue(value);
                return;
            }

            addChild(key, new KeyValue(key, value));
        }

        public static KeyValue readChunkfile(String path)
        {
            // Parse Valve chunkfile format
            KeyValue root = null;
            List<KeyValue> list = new List<KeyValue>();
            Stack<KeyValuePair<string, KeyValue>> stack = new Stack<KeyValuePair<string, KeyValue>>();

            if (File.Exists(path))
            {
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

                        string[] words = Steam.splitByWords(line);

                        if (words.Length > 0 && words[0].Contains("{")) // It opens a group
                        {
                            // We actually don't need to do anything.
                        }
                        else if (words.Length > 0 && words[0].Contains("}"))    // It closes a group
                        {
                            KeyValuePair<string, KeyValue> child = stack.Pop();
                            if (stack.Count > 0)
                                stack.Peek().Value.addChild(child.Key, child.Value);
                            else
                                list.Add(child.Value);
                                //root = child.Value;
                        }
                        else if (words.Length == 1 || words.Length > 1 && words[1].StartsWith("//"))    // It's a parent key
                        {
                            line = line.Replace("\"", "").ToLower();
                            stack.Push(new KeyValuePair<string, KeyValue>(line, new KeyValue(line)));
                        }
                        else if (words.Length >= 2) // It's a value key
                        {
                            string key = words[0].Replace("\"", "").ToLower();
                            KeyValue value = new KeyValue(key, words[1]);
                            stack.Peek().Value.addChild(key, value);
                        }
                    }
                }
            }

            if (list.Count > 1)
            {
                root = new KeyValue("");
                foreach(KeyValue keyValue in list)
                    root.addChild(keyValue);

                return root;
            } else
                return list[0];

        }

        public static void writeChunkFile(string path, KeyValue root)
        { 
            writeChunkFile(path, root, Encoding.UTF8);
        }

        public static void writeChunkFile(string path, KeyValue root, bool quotes)
        {
            writeChunkFile(path, root, quotes, Encoding.UTF8);
        }

        public static void writeChunkFile(string path, KeyValue root, Encoding encoding)
        {
            writeChunkFile(path, root, true, encoding);
        }

        public static void writeChunkFile(string path, KeyValue root, bool quotes, Encoding encoding)
        {
            List<string> lines = writeChunkFileTraverse(root, 0, quotes);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllLines(path, lines, encoding);
        }

        private static List<string> writeChunkFileTraverse(KeyValue node, int level, bool quotes)
        {
            List<string> lines = new List<string>();

            string tabs = "";
            for (int i = 0; i < level; i++)
            {
                tabs = tabs + "\t";
            }

            if (node.isParentKey())
            {
                if (node.key != "")
                {
                    lines.Add(tabs + (quotes ? "\"" : "") + node.key + (quotes ? "\"" : ""));
                    lines.Add(tabs + "{");
                }

                foreach (KeyValue entry in node.getChildrenList())
                    lines.AddRange(writeChunkFileTraverse(entry, level + 1, quotes));

                if (node.key != "")
                    lines.Add(tabs + "}");

                
            }
            else if (node.getValue() != null)
            {
                lines.Add(tabs + (quotes ? "\"" : "") + node.key + (quotes ? "\"" : "") + "\t\"" + node.value + "\"");
            }



            return lines;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OwLib
{
    /// <summary>
    /// TabConfigFileIO
    /// </summary>
    public static class TabConfigFileIO
    {

        /// <summary>
        /// 返回值结构
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, List<TabData>>> GetAllTabCfgData()
        {
            Dictionary<string, Dictionary<string, List<TabData>>> result =
                new Dictionary<string, Dictionary<string, List<TabData>>>();
            XmlDocument doc = new XmlDocument();
            try
            {
                string filePath = PathUtilities.CfgPath + "TabConfig.xml";
                doc.Load(filePath);
                result.Add("StockTab", GetNodeData("StockTab"));
                result.Add("InfoTab", GetNodeData("InfoTab"));
                result.Add("QuoteReport", GetNodeData("QuoteReport"));
                result.Add("TrendTab", GetNodeData("TrendTab"));
                result.Add("TrendMainTab", GetNodeData("TrendMainTab"));
                result.Add("TrendSubTab", GetNodeData("TrendSubTab"));
                result.Add("KlineMainTab", GetNodeData("KlineMainTab"));
                result.Add("KlineSubTab", GetNodeData("KlineSubTab"));
                result.Add("InfoPanelBottomTab", GetNodeData("InfoPanelBottomTab"));
                result.Add("InfoPanelTopTab", GetNodeData("InfoPanelTopTab"));
                result.Add("MainQuoteTab", GetNodeData("MainQuoteTab"));
                result.Add("MainQuoteTabButton", GetNodeData("MainQuoteTabButton"));
                result.Add("MainBlockMonitorTab", GetNodeData("MainBlockMonitorTab"));
                result.Add("MainBlockMonitorButton", GetNodeData("MainBlockMonitorButton"));
                result.Add("ViewTabBtn", GetNodeData("ViewTabBtn"));
            }
            catch (Exception)
            {
                LogUtilities.LogMessage("Read TabConfigFile Error");
                throw;
            }
            return result;
        }

        private static Dictionary<string, List<TabData>> GetNodeData(string nodeName)
        {
            Dictionary<string, List<TabData>> result = new Dictionary<string, List<TabData>>();
            XmlDocument doc = new XmlDocument();
            string filePath = PathUtilities.CfgPath + "TabConfig.xml";
            doc.Load(filePath);
            XmlNode root = doc.SelectSingleNode("Tabs/" + nodeName);
            if (root != null)
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Comment)
                        continue;

                    List<TabData> list = new List<TabData>();
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.NodeType == XmlNodeType.Element)
                        {
                            TabData tabData = new TabData();
                            tabData.Id = subNode.Attributes["id"].Value;
                            tabData.Index = Convert.ToInt32(subNode.Attributes["index"].Value);
                            tabData.Name = subNode.Attributes["name"].Value;
                            list.Add(tabData);
                        }
                    }
                    list.Sort(new IndexComparer());
                    result.Add(node.Name, list);
                }
            }
            return result;
        }

        private class IndexComparer : IComparer<TabData>
        {
            public int Compare(TabData tb1, TabData tb2)
            {
                return tb1.Index.CompareTo(tb2.Index);
            }
        }
    }
    /// <summary>
    /// TabData
    /// </summary>
    public class TabData
    {
        private string _id;
        private int _index;
        private string _name;

        /// <summary>
        /// Id
        /// </summary>
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Index
        /// </summary>
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public TabData()
        {
        }

        public TabData(int index, string id, string name)
        {
            _index = index;
            _id = id;
            _name = name;
        }
    }
}

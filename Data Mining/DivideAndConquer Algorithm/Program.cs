using System;
using System.Collections.Generic;
using System.Data;

namespace DivideConquer
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable table = new DataTable();

            Attribute outlook = new Attribute(Constants.Outlook, new List<Option>() { new Option(Constants.Sunny), new Option(Constants.Overcast), new Option(Constants.Rainy) });
            Attribute temperature = new Attribute(Constants.Temperature, new List<Option>() { new Option(Constants.Hot), new Option(Constants.Mild), new Option(Constants.Cool) });
            Attribute humidity = new Attribute(Constants.Humidity, new List<Option>() { new Option(Constants.High), new Option(Constants.Normal) });
            Attribute windy = new Attribute(Constants.Windy, new List<Option>() { new Option(Constants.False), new Option(Constants.True) });
            Attribute play = new Attribute(Constants.Play, new List<Option>() { new Option(Constants.Yes), new Option(Constants.No) });

            List<Attribute> attributes = new List<Attribute>() { outlook, temperature, humidity, windy };

            table.Columns.Add(outlook.attributeName, typeof(string));
            table.Columns.Add(temperature.attributeName, typeof(string));
            table.Columns.Add(humidity.attributeName, typeof(string));
            table.Columns.Add(windy.attributeName, typeof(string));
            table.Columns.Add(play.attributeName, typeof(string));
            
            #region Weather Data

            table.Rows.Add(Constants.Sunny, Constants.Hot, Constants.High, Constants.False, Constants.No);
            table.Rows.Add(Constants.Sunny, Constants.Hot, Constants.High, Constants.True, Constants.No);
            table.Rows.Add(Constants.Overcast, Constants.Hot, Constants.High, Constants.False, Constants.Yes);
            table.Rows.Add(Constants.Rainy, Constants.Mild, Constants.High, Constants.False, Constants.Yes);
            table.Rows.Add(Constants.Rainy, Constants.Cool, Constants.Normal, Constants.False, Constants.Yes);
            table.Rows.Add(Constants.Rainy, Constants.Cool, Constants.Normal, Constants.True, Constants.No);
            table.Rows.Add(Constants.Overcast, Constants.Cool, Constants.Normal, Constants.True, Constants.Yes);
            table.Rows.Add(Constants.Sunny, Constants.Mild, Constants.High, Constants.False, Constants.No);
            table.Rows.Add(Constants.Sunny, Constants.Cool, Constants.Normal, Constants.False, Constants.Yes);
            table.Rows.Add(Constants.Rainy, Constants.Mild, Constants.Normal, Constants.False, Constants.Yes);
            table.Rows.Add(Constants.Sunny, Constants.Mild, Constants.Normal, Constants.True, Constants.Yes);
            table.Rows.Add(Constants.Overcast, Constants.Mild, Constants.High, Constants.True, Constants.Yes);
            table.Rows.Add(Constants.Overcast, Constants.Hot, Constants.Normal, Constants.False, Constants.Yes);
            table.Rows.Add(Constants.Rainy, Constants.Mild, Constants.High, Constants.True, Constants.No);

            #endregion

            DecisionTree decisionTree = new DecisionTree();

            decisionTree.BuildDecisionTree(table, attributes, null);

            decisionTree.PrintDecisionTree(decisionTree.treeNodes[0]);

            Console.ReadKey();
        }
    }
}
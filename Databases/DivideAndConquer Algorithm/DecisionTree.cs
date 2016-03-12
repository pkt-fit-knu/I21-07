using System;
using System.Collections.Generic;
using System.Data;

namespace DivideConquer
{
    internal class DecisionTree
    {
        public List<TreeNode> treeNodes;

        public DecisionTree()
        {
            this.treeNodes = new List<TreeNode>();
        }

        public void BuildDecisionTree(DataTable table, List<Attribute> attributes, Relation relation)
        {
            for (int i = 0; i < table.Columns.Count - 1; i++)
            {
                foreach (Attribute attribute in attributes)
                {
                    if (table.Columns[i].Caption == attribute.attributeName)
                    {
                        for (int j = 0; j < table.Rows.Count; j++)
                        {
                            foreach (Option option in attribute.options)
                            {
                                if (table.Rows[j][i].ToString() == option.optionName)
                                {
                                    if (table.Rows[j].Field<string>("Play") == Constants.Yes)
                                    {
                                        option.yesAmount++;
                                    }
                                    else if (table.Rows[j].Field<string>("Play") == Constants.No)
                                    {
                                        option.noAmount++;
                                    }
                                }
                            }
                        }

                        foreach (Option option in attribute.options)
                        {
                            option.setInformationValue();

                            if (Double.IsNaN(option.informationValue))
                            {
                                option.informationValue = 0;
                            }

                            Console.WriteLine(option.informationValue);
                        }

                        attribute.setAverageInformationValue();

                        Console.WriteLine("      " + attribute.averageInformationValue);
                    }
                }
            }

            Attribute choosenAttribute = attributes[0];

            for (int i = 1; i < attributes.Count; i++)
            {
                if (attributes[i].averageInformationValue > choosenAttribute.averageInformationValue)
                {
                    choosenAttribute = attributes[i];
                }
            }

            Console.WriteLine("         " + choosenAttribute.averageInformationValue);

            Console.WriteLine(Environment.NewLine + Environment.NewLine);

            List<Relation> relations = new List<Relation>();

            DataTable dataTable;

            foreach (Option option in choosenAttribute.options)
            {
                if (option.informationValue != 0)
                {
                    dataTable = table.Copy();

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if (dataTable.Rows[i].Field<string>(choosenAttribute.attributeName) != option.optionName)
                        {
                            dataTable.Rows.Remove(dataTable.Rows[i]);
                            i--;
                        }
                    }

                    relations.Add(new Relation(null, option.optionName, dataTable));
                }
                else
                {
                    string nodeData = "";

                    if (option.yesAmount == 0)
                    {
                        nodeData = Constants.No;
                    }
                    else if (option.noAmount == 0)
                    {
                        nodeData = Constants.Yes;
                    }

                    relations.Add(new Relation(new TreeNode(nodeData, null, null), option.optionName, null));
                }
            }

            List<Attribute> tempAttributes = new List<Attribute>();

            foreach (Attribute attribute in attributes)
            {
                if (attribute != choosenAttribute)
                {
                    tempAttributes.Add(attribute);
                }
            }

            TreeNode tempTreeNode = new TreeNode(choosenAttribute.attributeName, relations, tempAttributes);

            this.treeNodes.Add(tempTreeNode);

            if (relation != null)
            {
                relation.childNode = tempTreeNode;
            }

            foreach (Attribute attribute in attributes)
            {
                foreach (Option option in attribute.options)
                {
                    option.yesAmount = 0;
                    option.noAmount = 0;
                }

                attribute.averageInformationValue = 0;
            }

            foreach (Relation newRelation in relations)
            {
                if (newRelation.childNode == null)
                {
                    BuildDecisionTree(newRelation.dataTable, tempTreeNode.attributes, newRelation);
                }
                else
                {
                    continue;
                }
            }
        }

        public void PrintDecisionTree(TreeNode treeNode)
        {
            if (treeNode.nodeData != Constants.Yes && treeNode.nodeData != Constants.No)
            {
                Console.WriteLine(treeNode.nodeData);
            }
            else
            {
                Console.WriteLine("          " + treeNode.nodeData);
            }

            if (treeNode.relations != null)
            {
                foreach (Relation relation in treeNode.relations)
                {
                    Console.WriteLine("     " + relation.childEdge);

                    PrintDecisionTree(relation.childNode);
                }
            }
        }
    }
}

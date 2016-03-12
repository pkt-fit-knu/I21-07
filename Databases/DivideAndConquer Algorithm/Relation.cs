using System.Data;

namespace DivideConquer
{
    internal class Relation
    {
        public TreeNode childNode;
        public string childEdge;

        public DataTable dataTable;

        public Relation(TreeNode childNode, string childEdge, DataTable dataTable)
        {
            this.childNode = childNode;
            this.childEdge = childEdge;

            this.dataTable = dataTable;
        }
    }
}

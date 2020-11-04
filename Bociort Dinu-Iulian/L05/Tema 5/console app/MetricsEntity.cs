using Microsoft.WindowsAzure.Storage.Table;

namespace Metrics
{
    public class MetricsEntity : TableEntity
    {
        public MetricsEntity(string university, string timestamp)
        {
            this.PartitionKey = university;
            this.RowKey = timestamp;
        }

        public MetricsEntity() { }

        public string Count { get; set; }

    }
}
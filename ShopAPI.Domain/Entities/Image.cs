using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

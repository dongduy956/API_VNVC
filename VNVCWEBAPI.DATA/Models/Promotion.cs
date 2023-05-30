using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.DATA.Models
{
    public class Promotion : BaseEntity
    {
        public Promotion()
        {
            ConditionPromotions = new HashSet<ConditionPromotion>();
        }
        public string PromotionCode { get; set; }
        public double Discount { get; set; }
        public int Count { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ConditionPromotion> ConditionPromotions { get; set; }
    }
}

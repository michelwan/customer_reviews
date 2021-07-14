using amazon_customer_reviews_data.dbcontext;
using System;

namespace amazon_customer_reviews_data.models
{
    public class Base
    {
        [IsUtcAttribute]
        public DateTime Created { get; set; }
        [IsUtcAttribute]
        public DateTime Updated { get; set; }
    }
}

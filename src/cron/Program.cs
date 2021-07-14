using amazon_customer_reviews_client;
using amazon_customer_reviews_client.Contracts;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace amazon_customer_reviews_cron
{
    class Program
    {
        static async Task Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration configuration = builder.Build();
            var config = new SettingsConfiguration();
            configuration.Bind(config);

            var strHtml = GetRequest(config.Parameters.ProductUrl);
            var customerReviews = DataParse(strHtml);

            using (var httpClient = new System.Net.Http.HttpClient())
            {
                ICustomerReviewsClient client = new CustomerReviewsClient(config.ApiSettings.AmazonCustomerReviews.BaseUrl, httpClient);
                foreach (var customerReview in customerReviews)
                    await client.CreateAsync(customerReview);
            }
        }

        public static string GetRequest(string url)
        {
            string strhtml = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko)";
            using (var response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                strhtml = reader.ReadToEnd();
            }
            return strhtml;
        }

        public static IList<CreateCustomerReviewRequest> DataParse(string strHtml)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(strHtml);
            htmlDoc.DocumentNode.Descendants()
                .Where(x => x.Name == "script" || x.Name == "style")
                .ToList()
                .ForEach(x => x.Remove());

            var reviews = new List<CreateCustomerReviewRequest>();
            var reviewsAsHtml = htmlDoc.DocumentNode.SelectNodes("//div[@id='cm_cr-review_list']/div[@data-hook='review']").ToList();
            //TODO retrieve comments from other pages
            foreach (var reviewAsHtml in reviewsAsHtml)
            {
                var review = new CreateCustomerReviewRequest();

                var userUrl = reviewAsHtml.SelectSingleNode(".//a[@class='a-profile']").Attributes.First(x => x.Name == "href").Value;
                userUrl = userUrl.Replace("/gp/profile/amzn1.account.", "");
                review.UserId = userUrl.Remove(userUrl.IndexOf("/"));
                review.UserName = reviewAsHtml.SelectSingleNode(".//span[@class='a-profile-name']").InnerText;
                review.Title = reviewAsHtml.SelectSingleNode(".//a[@data-hook='review-title']/span").InnerText;
                review.Comment = reviewAsHtml.SelectSingleNode(".//span[@data-hook='review-body']/span").InnerText;
                var asinUrl = reviewAsHtml.SelectSingleNode(".//a[@data-hook='review-title']").Attributes.First(x => x.Name == "href").Value;
                review.Asin = asinUrl.Remove(0, asinUrl.IndexOf("ASIN=") + 5);
                //TODO get note, date
                reviews.Add(review);
            }
            return reviews;
        }
    }
}

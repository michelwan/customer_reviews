namespace amazon_customer_reviews_cron
{
    public class SettingsConfiguration
    {
        public ApiSettings ApiSettings { get; set; }
        public Parameters Parameters { get; set; }
    }

    public class ApiSettings
    {
        public ApiSetting AmazonCustomerReviews { get; set; }
    }

    public class ApiSetting
    {
        public string BaseUrl { get; set; }
        public string Version { get; set; }
    }

    public class Parameters
    {
        public string ProductUrl { get; set; }
    }
}

namespace nylas_dotnet
{
    public class AccountDetail
    {
        public string? id { get; set; }
        public string? account_id { get; set; }
        public string? name { get; set; }
        public string? email_address { get; set; }
        public string? provider { get; set; }
        public string? organization_unit { get; set; }
        public string? sync_state { get; set; }
        public long? linked_at { get; set; }

        public override string ToString()
        {
            return $"AccountDetail [id={id}, account_id={account_id}, name={name}, email_address={email_address}, provider={provider}, organization_unit={organization_unit}, sync_state={sync_state}, linked_at={linked_at}]";
        }
    }
}
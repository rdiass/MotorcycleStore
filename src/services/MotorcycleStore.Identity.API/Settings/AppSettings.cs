namespace MotorcycleStore.Identity.API.Settings;

public class AppSettings
{
    public string Secret { get; init; }
    public int HoursToExpire { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
}

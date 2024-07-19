namespace Bookify.Domain.Apartments
{
    /// <summary>
    /// 地址值对象
    /// </summary>
    /// <param name="Country"></param>
    /// <param name="State"></param>
    /// <param name="ZipCode"></param>
    /// <param name="City"></param>
    /// <param name="Street"></param>
    public record Address(
        string Country,
        string State,
        string ZipCode,
        string City,
        string Street
    );
}

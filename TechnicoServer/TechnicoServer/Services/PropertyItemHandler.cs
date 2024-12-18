

using TechnicoDomain.Models;

namespace TechnicoDomain.Services;

public static class PropertyItemHandler
{
    /// <summary>
    /// Calculates the time period in days between today 
    /// and the repair creation
    /// </summary>
    /// <param name="repair"></param>
    /// <returns></returns>
    public static  int DaysElapsed(Repair repair)
    {
        return repair.Created.Subtract(DateTime.Today).Days;
    }
}

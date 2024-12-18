

using TechnicoDomain.Models;

namespace TechnicoDomain.Services;

public static class PropertyItemHandler
{

    public static int DaysElapsed(Repair repair)
    {
        if (repair.Created.HasValue)
        {
            return (DateTime.Today - repair.Created.Value).Days;
        }
        return 0; 
    }
}

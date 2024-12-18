

using TechnicoDomain.Models;

namespace TechnicoDomain.Services;

public class Analysis
{
    public string CityName { get; set; }
    public int Count { get; set; }
}

public  class ListManipulation
{
    private List<Owner> data = [
        new Owner { Name="Dimitris", Address="Athens"},
        new Owner { Name="George",Address="Salonica" },
        new Owner { Name="Nick",Address="Salonica" },
        new Owner { Name="Ratios",Address="Salonica" },
    ];


    public List<string> GetOwnerCities()
    {
       return  data
            .Select(owner => owner.Address)  
            .Distinct()
            .ToList();
    }

    public List<Analysis> CountOwnerCities()
    {
        return data
            .GroupBy(owner => owner.Address)
            .Select(city => new Analysis 
             {  CityName = city.Key,
                Count = city.Count()
             })
             .ToList();
    }

    public List<string> GetOwnerStartingFrom(string query)
    {
        return data
             .Where(owner => owner.Name.StartsWith(query))
             .Select(owner =>owner.Name)
             .OrderBy(name => name)
             .Distinct()
             .ToList();
    }


}

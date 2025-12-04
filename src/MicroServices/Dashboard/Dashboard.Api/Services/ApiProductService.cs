namespace Dashboard.Api.Services;

public class ApiProductService(HttpClient _http)
{
    public async Task<int> GetCount()
    {
        var response = await _http.GetAsync("api/products/count");

        int count = int.Parse(await response.Content.ReadAsStringAsync());

        return count;
    }
}



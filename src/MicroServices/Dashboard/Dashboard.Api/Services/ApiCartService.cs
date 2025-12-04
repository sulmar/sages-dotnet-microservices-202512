namespace Dashboard.Api.Services;

public class ApiCartService(HttpClient _http)
{
    public async Task<int> GetSessionsCount()
    {
        var response = await _http.GetAsync("api/cart/sessions/count");

        int count = int.Parse(await response.Content.ReadAsStringAsync());

        return count;
    }
}



using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DocumentApproval.API.IntegrationTests;

public class ApprovalFlowTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApprovalFlowTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    

    [Fact]
    public async Task Document_is_approved_when_all_steps_are_approved_in_order()
    {
        // 1. Create document
        var createResponse = await _client.PostAsJsonAsync(
            "/documents",
            new
            {
                title = "Expense Policy",
                approvalSteps = new[]
                {
                    new { stepOrder = 1, approverUserId = "user-1" },
                    new { stepOrder = 2, approverUserId = "user-2" }
                }
            });

        createResponse.EnsureSuccessStatusCode();

        var documentId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        // 2. Submit document
        var submitResponse = await _client.PostAsync(
            $"/documents/{documentId}/submit",
            null);

        submitResponse.EnsureSuccessStatusCode();

        // 3. Approve step 1
        _client.DefaultRequestHeaders.Add("X-Test-User", "user-1");

        var approveStep1 = await _client.PostAsync(
            $"/documents/{documentId}/approve",
            null);

        approveStep1.EnsureSuccessStatusCode();

        // 4. Approve step 2
        _client.DefaultRequestHeaders.Remove("X-Test-User");
        _client.DefaultRequestHeaders.Add("X-Test-User", "user-2");

        var approveStep2 = await _client.PostAsync(
            $"/documents/{documentId}/approve",
            null);

        approveStep2.EnsureSuccessStatusCode();

        // 5. Get document
        var document = await _client.GetFromJsonAsync<dynamic>(
            $"/documents/{documentId}");

        Assert.Equal("Approved", (string)document.status);
    }    
}
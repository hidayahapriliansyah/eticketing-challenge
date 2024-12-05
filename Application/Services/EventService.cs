using eticketing.Http.Requests;
using eticketing.Http.Responses;
using eticketing.Infrastructure.Repository;

namespace eticketing.Application.Services;

public class EventService(EventRepository eventRepository)
{
    private EventRepository _eventRepository = eventRepository;

    public async Task<ApiResponse<GetEventResponse>> GetEventsAsync(
        GetEventsRequest request,
        bool isAdmin
    )
    {
        var (events, totalData, totalPages) = await _eventRepository.GetEventAsync(
            request,
            isAdmin
        );

        var response = new ApiResponse<GetEventResponse>
        {
            Success = true,
            Message = "success to get events",
            Data = new GetEventResponse { Events = events },
            Pagination = new Pagination
            {
                TotalData = totalData,
                TotalPages = totalPages,
                CurrentPage = request.Page,
                PageSize = request.Limit,
            },
        };
        return response;
    }
}

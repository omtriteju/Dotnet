using Groko.core.Services;
using Groko.Core.Entities;
using Groko.Core.QueryModels;
using Microsoft.AspNetCore.Mvc;

namespace Groko.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("GetAllEvents")]
        public async Task<ActionResult<IEnumerable<Events>>> GetEvents([FromBody] EventFilter filter)
        {
            var events = await _eventService.GetEventsAsync(filter);
            return Ok(events);
        }


        [HttpPost("CreateEvent")]
        public async Task<ActionResult> CreateEvent([FromBody] Events eventDetails)
        {
            await _eventService.CreateEventAsync(eventDetails);
            return Ok();
        }

        [HttpPut("UpdateEvent")]
        public async Task<ActionResult> UpdateEvent([FromBody] Events eventDetails)
        {
            await _eventService.UpdateEventAsync(eventDetails);
            return Ok();
        }

        [HttpDelete("DeleteEvent/{eventGUID}")]
        public async Task<ActionResult> DeleteEvent(Guid eventGUID)
        {
            await _eventService.DeleteEventAsync(eventGUID);
            return Ok();
        }

        [HttpPost("BookmarkEvent")]
        public async Task<ActionResult> BookmarkEvent([FromBody] UserBookmark bookmark)
        {
            await _eventService.AddBookmarkAsync(bookmark);
            return Ok();
        }

        [HttpDelete("RemoveBookmark/{userID}/{eventGUID}")]
        public async Task<ActionResult> RemoveBookmark(Guid userID, Guid eventGUID)
        {
            await _eventService.RemoveBookmarkAsync(userID, eventGUID);
            return Ok();
        }

        [HttpGet("GetBookmarkedEvents/{userID}")]
        public async Task<ActionResult<IEnumerable<Events>>> GetBookmarkedEvents(Guid userID)
        {
            var events = await _eventService.GetBookmarkedEventsAsync(userID);
            return Ok(events);
        }

        [HttpPost("SuggestEvent")]
        public async Task<ActionResult> SuggestEvent([FromBody] Events eventDetails)
        {
            await _eventService.SuggestEventAsync(eventDetails);
            return Ok();
        }

        [HttpGet("GetSuggestedEvents")]
        public async Task<ActionResult<IEnumerable<Events>>> GetSuggestedEvents()
        {
            var events = await _eventService.GetSuggestedEventsAsync();
            return Ok(events);
        }

        // [HttpPut("UpdateSuggestedEvent")]
        // public async Task<ActionResult> UpdateSuggestedEvent([FromBody] Events eventDetails)
        // {
        //     await _eventService.UpdateSuggestedEventAsync(eventDetails);
        //     return Ok();
        // }

        [HttpPost("ApproveSuggestedEvent/{suggestedEventID}")]
        public async Task<ActionResult> ApproveSuggestedEvent(Guid suggestedEventID)
        {
            await _eventService.ApproveSuggestedEventAsync(suggestedEventID);
            return Ok();
        }









    }
}

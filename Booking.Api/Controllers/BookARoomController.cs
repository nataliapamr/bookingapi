using Booking.Domain.Models;
using Booking.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookARoomController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        
        public BookARoomController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Get all bookings for the hotel.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public ActionResult<Book> GetAll()
        {
            IEnumerable<Book> result;
            try
            {
                result = _bookingService.GetAllBookings();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get a specific book by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getById/{id}")]
        public ActionResult<Book> GetById(string id)
        {
            Book result;
            try
            {
                result = _bookingService.GetBookingById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        /// <summary>
        /// Book a room at the hotel.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost("book")]
        public ActionResult Create(Book book)
        {
            try
            {
                _bookingService.PlaceBooking(book);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Update a booking that.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookingIn"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Edit(string id, Book book)
        {
            try
            {
                _bookingService.EditBooking(id, book);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Delete a booking.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                _bookingService.DeleteBooking(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}

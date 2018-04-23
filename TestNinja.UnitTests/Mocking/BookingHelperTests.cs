using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class BookingHelperTests
    {
        private Booking _existingBooking;
        private Mock<IBookingRpository> _repository;

        public BookingHelperTests()
        {
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };    
            
            _repository = new Mock<IBookingRpository>();
            _repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking                
            }.AsQueryable());            
        }

        [Fact]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {


            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                    DepartureDate = Before(_existingBooking.ArrivalDate),
                    Reference =  "b"
                }
                , _repository.Object);

            Assert.Equal(string.Empty, result);
        }
        
        [Fact]
        public void BookingStartsBeforAnExistingBookingAndFinishesInTheMiddleOfExistingBooking_ReturnExistingBookingReference()
        {


            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.ArrivalDate),
                    Reference =  "b"
                }
                , _repository.Object);

            Assert.Equal(_existingBooking.Reference, result);
        } 
        
        [Fact]
        public void BookingStartsBeforAnExistingBookingAndFinishesAferAnExistingBooking_ReturnExistingBookingReference()
        {


            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                    Reference =  "b"
                }
                , _repository.Object);

            Assert.Equal(_existingBooking.Reference, result);
        }           

        [Fact]
        public void BookingStartsAndFinishesInTheMiddleOfExistingBooking_ReturnExistingBookingReference()
        {


            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = Before(_existingBooking.DepartureDate),
                    Reference =  "b"
                }
                , _repository.Object);

            Assert.Equal(_existingBooking.Reference, result);
        } 
        
        [Fact]
        public void BookingStartsInTheMiddleOfExistingBookingAndAndsAfter_ReturnExistingBookingReference()
        {


            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                    Reference =  "b"
                }
                , _repository.Object);

            Assert.Equal(_existingBooking.Reference, result);
        }
        
        
        [Fact]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {


            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.DepartureDate),
                    DepartureDate = After(_existingBooking.DepartureDate, days: 2),
                    Reference =  "b"
                }
                , _repository.Object);

            Assert.Equal(string.Empty, result);
        } 
        
        [Fact]
        public void BookingsOverlapButNewBookingIsCanceled_ReturnEmptyString()
        {


            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                    Status = "Cancelled",
                    Reference =  "b"
                }
                , _repository.Object);

            Assert.Equal(string.Empty, result);
        }        
        
        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }        
        
        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
        
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
        
    }
}
using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;

namespace Bookify.Application.Bookings.GetBooking
{
    internal sealed class GetBookingQueryHandler : IQueryHandler<GetBookingQuery, BookingResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IUserContext _userContext;

        public GetBookingQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _userContext = userContext;
        }

        public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            const string sql = """
                Select 
                    id As Id,
                    apartment_id As ApartmentId,
                    user_id As UserId,
                    status As Status,
                    price_for_period_amount As PriceAmount,
                    price_for_period_currency As PriceCurrency,
                    cleaning_fee_amount As CleaningFeeAmount,
                    cleaning_fee_currency As CleaningFeeCurrency,
                    amenities_up_charge_amount As AmenitiesUpChargeAmount,
                    amenities_up_charge_currency As AmenitiesUpChargeCurrency,
                    total_price_amount As TotalPriceAmount,
                    total_price_currency As TotalPriceCurrency,
                    duration_start As DurationStart,
                    duration_end As DurationEnd,
                    created_on_utc As CreatedOnUtc
                From bookings
                Where id = @BookingId
            """;

            var booking = await connection.QueryFirstOrDefaultAsync<BookingResponse>(
                sql,
                new
                {
                    request.BookingId
                }
            );

            if (booking is null || booking.UserId != _userContext.UserId)
            {
                return Result.Failure<BookingResponse>(BookingErrors.NotFound);
            }

            return booking;
        }
    }
}

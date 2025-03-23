using Kata.Api.Controllers;
using Kata.BLL;
using Kata.DAL.Data;
using Shouldly;

namespace Kata.Tests;

public class BookingShould
{
    [Fact]
    public void Reserve_bar_when_at_least_60_percent_of_devs_are_available()
    {
        // Arrange
        var indoorBarName = "Bar La Belle Equipe";
        var indoorBars = new[]
        {
            ABar() with
            {
                Name = indoorBarName, Open = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }
            }
        };

        var developers = new[]
        {
            new DeveloperData { Name = "Alice", OnSite = new[] { _wednesday, _thursday, _friday } },
            new DeveloperData { Name = "Bob", OnSite = new[] { _thursday } },
            new DeveloperData { Name = "Chad", OnSite = new[] { _friday } },
            new DeveloperData { Name = "Dan", OnSite = new[] { _wednesday, _thursday } },
            new DeveloperData { Name = "Eve", OnSite = new[] { _thursday } },
        };

        // Act
        var controller = BuildController(indoorBars, developers);
        controller.MakeBooking();
        var result = controller.Get().Single();

        // Assert
        result.Date.ShouldBe(_thursday);
        result.Bar.Name.ShouldBe(indoorBarName);
    }

    [Fact]
    public void Do_not_reserve_bar_when_only_50_percent_of_devs_are_available()
    {
        // Arrange
        var indoorBars = new[]
        {
            ABar() with { Open = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday } }
        };

        var developers = new[]
        {
            new DeveloperData { Name = "Alice", OnSite = new[] { _wednesday, _friday } },
            new DeveloperData { Name = "Bob", OnSite = new[] { _thursday } },
            new DeveloperData { Name = "Chad", OnSite = new[] { _friday } },
            new DeveloperData { Name = "Dan", OnSite = new[] { _wednesday } },
            new DeveloperData { Name = "Eve", OnSite = new[] { _thursday } }
        };

        // Act
        var controller = BuildController(indoorBars, developers);
        var result = controller.MakeBooking();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void Reserve_bar_when_it_is_open()
    {
        // Arrange
        var indoorBarName = "Bar La Belle Equipe";
        var indoorBars = new[]
        {
            ABar() with { Name = indoorBarName, Open = new[] { DayOfWeek.Thursday } },
            ABar() with { Name = "Le Sirius", Open = new[] { DayOfWeek.Friday } }
        };
        var developers = new[]
        {
            new DeveloperData { Name = "Bob", OnSite = new[] { _thursday } },
            new DeveloperData { Name = "Alice", OnSite = new[] { _thursday } }
        };

        // Act
        var controller = BuildController(indoorBars, developers);
        controller.MakeBooking();
        var result = controller.Get().Single();

        // Assert
        result.Date.ShouldBe(_thursday);
        result.Bar.Name.ShouldBe(indoorBarName);
    }

    [Fact]
    public void Do_not_reserve_bar_when_it_is_closed()
    {
        // Arrange
        var indoorBars = new[]
        {
            ABar() with { Name = "La belle équipe", Open = new[] { DayOfWeek.Thursday } },
            ABar() with { Name = "Le Sirius", Open = new[] { DayOfWeek.Friday } }
        };
        var developers = new[]
        {
            new DeveloperData { Name = "Bob", OnSite = new[] { _wednesday } },
            new DeveloperData { Name = "Alice", OnSite = new[] { _wednesday } }
        };

        // Act
        var controller = BuildController(indoorBars, developers);
        var result = controller.MakeBooking();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void Choose_bar_that_has_enough_space()
    {
        // Arrange
        var indoorBars = new[]
        {
            ABar() with { Capacity = 3 }
        };
        var developers = new[]
        {
            new DeveloperData { Name = "Bob", OnSite = new[] { _wednesday, _friday } },
            new DeveloperData { Name = "Chad", OnSite = new[] { _wednesday } },
            new DeveloperData { Name = "Dan", OnSite = new[] { _wednesday } },
            new DeveloperData { Name = "Eve", OnSite = new[] { _wednesday } },
        };

        // Act
        var controller = BuildController(indoorBars, developers);
        var result = controller.MakeBooking();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact(Skip = "Not implemented yet")]
    public void Choose_boat_over_bar_when_available()
    {
        // Arrange
        var barData = new[] { ABar() with { Open = new[] { DayOfWeek.Wednesday } } };
        var devData = new DeveloperData[]
        {
            new() { Name = "Bob", OnSite = new[] { _wednesday } },
            new() { Name = "Alice", OnSite = new[] { _wednesday } },
        };
        var boatName = "Péniche Ayers Rock";
        var boatData = new BoatData[]
            { new() { MaxPeople = 3, Name = boatName } };

        // Act
        var endpoint = BuildController(barData, devData, boatData);
        endpoint.MakeBooking();
        var booking = endpoint.Get().Single();

        // Assert
        booking.Date.ShouldBe(_wednesday);
        booking.Bar.Name.ShouldBe(boatName);
    }

    private static BookingController BuildController(
        BarData[] bars,
        DeveloperData[] developers,
        BoatData[]? boats = null)
    {
        var bookingRepository = new FakeBookingRepository();

        return new BookingController(
            new BookingService(
                new FakeBarRepository(bars),
                new FakeDeveloperRepository(developers),
                new FakeBoatRepository(boats ?? Array.Empty<BoatData>()),
                bookingRepository),
            bookingRepository);
    }

    private static BarData ABar() => new(
        Name: "Les Etoiles",
        Capacity: 10,
        Open: new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }
        );

    private static readonly DateTime _wednesday = new(2025, 05, 14);
    private static readonly DateTime _thursday = _wednesday.AddDays(1);
    private static readonly DateTime _friday = _wednesday.AddDays(2);
}

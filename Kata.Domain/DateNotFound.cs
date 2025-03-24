namespace Kata.Domain;

internal record DateNotFound() : BestDate(DateTime.MinValue, 0);

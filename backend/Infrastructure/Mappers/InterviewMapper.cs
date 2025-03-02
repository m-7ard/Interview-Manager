using Domain.Contracts.Models.Interview;
using Domain.Models;
using Infrastructure.DbEntities;

namespace Infrastructure.Mappers;

public static class InterviewMapper
{
    public static Interview DbEntityToDomain(InterviewDbEntity source)
    {
        var contract = new CreateInterviewContract(
            id: source.Id,
            venue: source.Venue,
            status: source.Status,
            dateScheduled: source.DateScheduled,
            dateStarted: source.DateStarted,
            dateFinished: source.DateFinished,
            interviewer: source.Interviewer
        );

        return Interview.ExecuteCreate(contract);
    }

    public static InterviewDbEntity DomainToDbEntity(Interview source)
    {
        return new InterviewDbEntity(
            id: source.Id.Value,
            venue: source.Venue,
            status: source.Schedule.Status.Value,
            dateScheduled: source.Schedule.Dates.DateScheduled,
            dateStarted: source.Schedule.Dates.DateStarted,
            dateFinished: source.Schedule.Dates.DateFinished,
            interviewer: source.Interviewer
        );
    }
}
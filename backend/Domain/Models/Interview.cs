using Domain.Abstract;
using Domain.Contracts.Interview.ValueObjects;
using Domain.Contracts.Models.Interview;
using Domain.ValueObjects.Interviews;
using OneOf;

namespace Domain.Models;

public class Interview : DomainEntity<InterviewId>
{
    private Interview(InterviewId id, string? venue, InterviewSchedule schedule, string interviewer) : base(id)
    {
        Id = id;
        Venue = venue;
        Schedule = schedule;
        Interviewer = interviewer;
    }

    public string? Venue { get; private set; }
    public InterviewSchedule Schedule { get; private set; }
    public string Interviewer { get; private set; }
    
    public static OneOf<bool, string> CanCreate(CreateInterviewContract contract)
    {
        var idResult = InterviewId.CanCreate(contract.Id);
        if (idResult.TryPickT1(out var error, out _))
        {
            return error;
        }

        var statusResult = InterviewStatus.TryCreate(contract.Status);
        if (statusResult.TryPickT1(out error, out var status))
        {
            return error;
        }

        var datesResult = InterviewDates.TryCreate(new CreateInterviewDatesContract(dateScheduled: contract.DateScheduled, dateStarted: contract.DateStarted, dateFinished: contract.DateFinished));
        if (datesResult.TryPickT1(out error, out var dates))
        {
            return error;
        }

        var scheduleResult = InterviewSchedule.TryCreate(status: status, dates: dates);
        if (scheduleResult.TryPickT1(out error, out _))
        {
            return error;
        }

        return true;
    }

    public static Interview ExecuteCreate(CreateInterviewContract contract)
    {
        var canCreateResult = CanCreate(contract);
        if (canCreateResult.TryPickT1(out var error, out _))
        {
            throw new Exception(error);
        }

        var status = InterviewStatus.ExecuteCreate(contract.Status);
        var dates = InterviewDates.ExecuteCreate(new CreateInterviewDatesContract(
            dateScheduled: contract.DateScheduled,
            dateStarted: contract.DateStarted,
            dateFinished: contract.DateFinished
        ));

        return new Interview(
            id: InterviewId.ExecuteCreate(contract.Id), 
            venue: contract.Venue, 
            schedule: InterviewSchedule.ExecuteCreate(
                status: status,
                dates: dates
            ),
            interviewer: contract.Interviewer
        );
    }

    public static OneOf<Interview, string> TryCreate(CreateInterviewContract contract)
    {
        var canCreateResult = CanCreate(contract);
        if (canCreateResult.TryPickT1(out var error, out _))
        {
            return error;
        }

        return ExecuteCreate(contract);
    }
}
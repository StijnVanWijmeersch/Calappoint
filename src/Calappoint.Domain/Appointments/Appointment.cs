using Calappoint.Domain.Appointments.Events;
using Calappoint.SharedKernel;

namespace Calappoint.Domain.Appointments;

public sealed class Appointment : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string ClientName { get; private set; }
    public string ClientEmail { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime StartTimeUtc { get; private set; }
    public DateTime EndTimeUtc { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    private Appointment() { }

    public static Appointment Create(Guid userId, string clientName, string clientEmail, DateTime date, DateTime startTimeUtc, DateTime endTimeUtc)
    {
        var appointment =  new Appointment
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ClientName = clientName,
            ClientEmail = clientEmail,
            Date = date,
            StartTimeUtc = startTimeUtc,
            EndTimeUtc = endTimeUtc,
            Status = AppointmentStatus.Scheduled,
            CreatedOnUtc = DateTime.UtcNow
        };

        appointment.RaiseDomainEvent(new AppointmentScheduledDomainEvent(appointment.Id));

        return appointment;
    }

    public Result CancelAppointment()
    {
        if (Status == AppointmentStatus.Cancelled)
        {
            return Result.Failure(AppointmentErrors.AppointmentAlreadyCancelled);
        }

        Status = AppointmentStatus.Cancelled;

        RaiseDomainEvent(new AppointmentCancelledDomainEvent(Id));

        return Result.Success();
    }

    public Result CompleteAppointment()
    {

       if (Status == AppointmentStatus.Completed)
       {
           return Result.Failure(AppointmentErrors.AppointmentAlreadyCompleted);
       }

        Status = AppointmentStatus.Completed;

        return Result.Success();
    }

    public Result RescheduleAppointment(DateTime newDate, DateTime newStartTimeUtc, DateTime newEndTimeUtc)
    {
        Date = newDate;
        StartTimeUtc = newStartTimeUtc;
        EndTimeUtc = newEndTimeUtc;

        RaiseDomainEvent(new AppointmentRescheduledDomainEvent(Id));

        return Result.Success();
    }
   
}

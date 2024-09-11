using Calappoint.Domain.Appointments.Events;
using Calappoint.Domain.Availabilities;
using Calappoint.Domain.Users;
using Calappoint.SharedKernel;

namespace Calappoint.Domain.Appointments;

public sealed class Appointment : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string ClientName { get; private set; }
    public string ClientEmail { get; private set; }
    public Guid AvailabilityId { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public User User { get; private set; }
    public Availability Availability { get; private set; }

    private Appointment() { }

    public static Appointment Create(User user, string clientName, string clientEmail, Availability availability)
    {
        var appointment =  new Appointment
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            ClientName = clientName,
            ClientEmail = clientEmail,
            AvailabilityId = availability.Id,
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

        Availability.UnBook();

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

    public Result RescheduleAppointment(Availability availability)
    {
        Availability.UnBook();

        AvailabilityId = availability.Id;
 
        RaiseDomainEvent(new AppointmentRescheduledDomainEvent(Id));

        return Result.Success();
    }
   
}

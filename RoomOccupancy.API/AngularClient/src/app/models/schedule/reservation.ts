export class Reservation {
  start: Date;
  end: Date;

  /// <summary>
  /// Name for the reservation label.
  /// </summary>
  subject: string;
  /// <summary>
  ///  Days of the week on which the cyclic reservation takes place.
  /// </summary>
  reservationDays: [];

  /// <summary>
  /// Will automatically book another meeting.
  /// </summary>
  isCyclical: boolean;

  /// <summary>
  /// The room where the meeting is to be held.
  /// </summary>
  roomId: number;
}

﻿using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.DataBaseEntities;

public class NotificationOutbox
{
    public long Id { get; set; }
    public NotificationType Type { get; set; }
    public int NotificationValue { get; set; }
}
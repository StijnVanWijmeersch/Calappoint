﻿namespace Calappoint.Application.Abstractions.Identity;

public sealed record UserModel(
    string Email,
    string Password
);

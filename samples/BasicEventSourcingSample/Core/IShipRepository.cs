// Copyright (c) IEvangelist. All rights reserved.
// Licensed under the MIT License.

namespace BasicEventSourcingSample.Core;

public interface IShipRepository
{
    ValueTask CreateShip(Ship ship);
    ValueTask<Ship> FindAsync(string shipName);
    ValueTask SaveAsync(Ship ship);
    Task<IEnumerable<string>> GetShipNamesAsync();
}
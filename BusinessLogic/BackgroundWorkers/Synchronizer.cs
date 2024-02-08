﻿using Coravel.Invocable;
using Shared.DataAccess.Repositories;

namespace BusinessLogic.BackgroundWorkers;

public class Synchronizer : IInvocable
{
    private SynchronizedRepository _synchronizedRepository;

    public Synchronizer(SynchronizedRepository synchronizedRepository)
    {
        _synchronizedRepository = synchronizedRepository;
    }
    

    public async Task Invoke()
    {
        await _synchronizedRepository.SynchronizeTournament();
        await _synchronizedRepository.SynchronizeTask();
    }
}
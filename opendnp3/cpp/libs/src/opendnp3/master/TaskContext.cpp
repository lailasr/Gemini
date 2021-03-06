/*
 * Copyright 2013-2019 Automatak, LLC
 *
 * Licensed to Green Energy Corp (www.greenenergycorp.com) and Automatak
 * LLC (www.automatak.com) under one or more contributor license agreements.
 * See the NOTICE file distributed with this work for additional information
 * regarding copyright ownership. Green Energy Corp and Automatak LLC license
 * this file to you under the Apache License, Version 2.0 (the "License"); you
 * may not use this file except in compliance with the License. You may obtain
 * a copy of the License at:
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#include "TaskContext.h"

#include "opendnp3/master/IMasterTask.h"

namespace opendnp3
{

void TaskContext::AddBlock(const IMasterTask& task)
{
    this->blocking_tasks.insert(&task);
}

void TaskContext::RemoveBlock(const IMasterTask& task)
{
    this->blocking_tasks.erase(&task);
}

bool TaskContext::IsBlocked(const IMasterTask& task) const
{
    for (auto& blocking : this->blocking_tasks)
    {
        // is there a block with better priority that's not the same task?
        if (blocking->Priority() < task.Priority() && (blocking != &task))
            return true;
    }

    return false;
}

} // namespace opendnp3

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

#include "MockLogHandler.h"

#include <openpal/logging/LogLevels.h>

#include <iostream>

using namespace openpal;

namespace testlib
{

LogRecord::LogRecord(const LogEntry& entry)
    : id(entry.loggerid), filters(entry.filters), location(entry.location), message(entry.message)
{
}

void MockLogHandlerImpl::Log(const LogEntry& entry)
{
    std::lock_guard<std::mutex> lock(this->mutex);

    if (outputToStdIO)
    {
        std::cout << entry.loggerid << " - " << entry.message << std::endl;
    }

    this->messages.emplace_back(entry);
}

void MockLogHandler::ClearLog()
{
    this->impl->messages.clear();
}

void MockLogHandler::Log(const std::string& location, const std::string& message)
{
    this->impl->Log(LogEntry("test", openpal::logflags::EVENT, location.c_str(), message.c_str()));
}

void MockLogHandler::WriteToStdIo()
{
    this->impl->outputToStdIO = true;
}

bool MockLogHandler::GetNextEntry(LogRecord& record)
{
    if (impl->messages.empty())
        return false;

    record = impl->messages.front();
    impl->messages.pop_front();
    return true;
}

} // namespace testlib

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
#ifndef ASIODNP3_ISTACK_H
#define ASIODNP3_ISTACK_H

#include "asiopal/IResourceManager.h"

#include <opendnp3/StackStatistics.h>

namespace asiodnp3
{

/**
 * Base class for masters or outstations
 */
class IStack : public asiopal::IResource
{
public:
    virtual ~IStack() {}

    /**
     * Synchronously enable communications
     */
    virtual bool Enable() = 0;

    /**
     * Synchronously disable communications
     */
    virtual bool Disable() = 0;

    /**
     * @return stack statistics counters
     */
    virtual opendnp3::StackStatistics GetStackStatistics() = 0;
};

} // namespace asiodnp3

#endif

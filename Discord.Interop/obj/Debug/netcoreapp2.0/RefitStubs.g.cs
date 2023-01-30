﻿// <auto-generated />
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Eynwa.Models.Entities.Stats;
using Refit;
using System.Text;
using System.Threading.Tasks;
using Eynwa.Models.Twitch;
using EynwaDiscordBot.Models.Constants;
using EynwaDiscordBot.Models.Entities.Account;

/* ******** Hey You! *********
 *
 * This is a generated file, and gets rewritten every time you build the
 * project. If you want to edit it, you need to edit the mustache template
 * in the Refit package */

#pragma warning disable
namespace Eynwa.Interop.RefitInternalGenerated
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    sealed class PreserveAttribute : Attribute
    {

        //
        // Fields
        //
        public bool AllMembers;

        public bool Conditional;
    }
}
#pragma warning restore

namespace Eynwa.Interop.Services
{
    using Eynwa.Interop.RefitInternalGenerated;

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
    [Preserve]
    partial class AutoGeneratedIStatsService : IStatsService        {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedIStatsService(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        public virtual Task<GameSessions> Create(GameSessions param)
        {
            var arguments = new object[] { param };
            var func = requestBuilder.BuildRestResultFuncForMethod("Create", new Type[] { typeof(GameSessions) });
            return (Task<GameSessions>)func(Client, arguments);
        }

        /// <inheritdoc />
        public virtual Task<GameSessions> GetSession(string uid)
        {
            var arguments = new object[] { uid };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetSession", new Type[] { typeof(string) });
            return (Task<GameSessions>)func(Client, arguments);
        }

        /// <inheritdoc />
        public virtual Task<List<GameSessions>> GetAllSessions(string dateStart,string dateEnd,string userId,string gameName)
        {
            var arguments = new object[] { dateStart,dateEnd,userId,gameName };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetAllSessions", new Type[] { typeof(string),typeof(string),typeof(string),typeof(string) });
            return (Task<List<GameSessions>>)func(Client, arguments);
        }

        /// <inheritdoc />
        public virtual Task<GameSessions> PatchSessions(string uid,GameSessions param)
        {
            var arguments = new object[] { uid,param };
            var func = requestBuilder.BuildRestResultFuncForMethod("PatchSessions", new Type[] { typeof(string),typeof(GameSessions) });
            return (Task<GameSessions>)func(Client, arguments);
        }

    }
}

namespace Eynwa.Interop.Services
{
    using Eynwa.Interop.RefitInternalGenerated;

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
    [Preserve]
    partial class AutoGeneratedITwitchApiService : ITwitchApiService        {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedITwitchApiService(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        public virtual Task PatchSessions(SessionParameters param)
        {
            var arguments = new object[] { param };
            var func = requestBuilder.BuildRestResultFuncForMethod("PatchSessions", new Type[] { typeof(SessionParameters) });
            return (Task)func(Client, arguments);
        }

        /// <inheritdoc />
        public virtual Task<GameInfo> GetGameInfos(string name)
        {
            var arguments = new object[] { name };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetGameInfos", new Type[] { typeof(string) });
            return (Task<GameInfo>)func(Client, arguments);
        }

    }
}

namespace Discord.Interop.Services
{
    using Eynwa.Interop.RefitInternalGenerated;

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
    [Preserve]
    partial class AutoGeneratedIUserService : IUserService        {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedIUserService(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        public virtual Task<UserInfo> Create(UserInfo param)
        {
            var arguments = new object[] { param };
            var func = requestBuilder.BuildRestResultFuncForMethod("Create", new Type[] { typeof(UserInfo) });
            return (Task<UserInfo>)func(Client, arguments);
        }

        /// <inheritdoc />
        public virtual Task<UserInfo> GetUser(string uid)
        {
            var arguments = new object[] { uid };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetUser", new Type[] { typeof(string) });
            return (Task<UserInfo>)func(Client, arguments);
        }

        /// <inheritdoc />
        public virtual Task<List<UserInfo>> GetAllUsers()
        {
            var arguments = new object[] {  };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetAllUsers", new Type[] {  });
            return (Task<List<UserInfo>>)func(Client, arguments);
        }

        /// <inheritdoc />
        public virtual Task<UserInfo> PutUser(long uid,UserInfo param)
        {
            var arguments = new object[] { uid,param };
            var func = requestBuilder.BuildRestResultFuncForMethod("PutUser", new Type[] { typeof(long),typeof(UserInfo) });
            return (Task<UserInfo>)func(Client, arguments);
        }

    }
}
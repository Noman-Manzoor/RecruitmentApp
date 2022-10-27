//using AutoMapper;
//using System.Security.Claims;

//namespace RecruitmentApp.Infrastructure
//{
//    public class CustomUserStore :
//        IUserLoginStore<CustomUser, string>,
//        IUserClaimStore<CustomUser, string>,
//        IUserPasswordStore<CustomUser, string>,
//        IUserSecurityStampStore<CustomUser, string>,
//        IUserEmailStore<CustomUser, string>,
//        IUserLockoutStore<CustomUser, string>,
//        IUserPhoneNumberStore<CustomUser, string>,
//        IUserTwoFactorStore<CustomUser, string>,
//        //IUserRoleStore<CustomUser, string>,
//        IUserStore<CustomUser, string>,
//        IQueryableUserStore<CustomUser, string>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;
//        //private const string InternalLoginProvider = "[AspNetUserStore]";
//        //private const string AuthenticatorKeyTokenName = "AuthenticatorKey";
//        //private const string RecoveryCodeTokenName = "RecoveryCodes";

//        public CustomUserStore(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }

//        public IQueryable<CustomUser> Users
//        {
//            get { return DBtoVMMapping(_unitOfWork.Repository<AspNetUser>().Entities).AsQueryable(); }
//        }

//        #region Users

//        public Task CreateAsync(CustomUser user)
//        {
//            try
//            {
//                if (user == null)
//                {
//                    throw new ArgumentNullException(nameof(user));
//                }

//                SaveCustomer(user);

//                return Task.CompletedTask;
//            }
//            catch (Exception)
//            {
//                return Task.CompletedTask;
//            }
//        }

//        public Task DeleteAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            var getUser = _unitOfWork.Repository<AspNetUser>().GetFirst(x => x.Id.Equals(user.Id));

//            _unitOfWork.Repository<AspNetUser>().Remove(getUser);

//            try
//            {
//                _unitOfWork.SaveChanges();
//                return Task.CompletedTask;
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return Task.CompletedTask;
//            }
//        }

//        public void Dispose()
//        {

//        }

//        public Task UpdateAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            var getUser = _unitOfWork.Repository<AspNetUser>().GetFirst(x => x.Id.Equals(user.Id));

//            getUser.AccessFailedCount = user.AccessFailedCount;
//            getUser.ConcurrencyStamp = user.ConcurrencyStamp;
//            getUser.Email = user.Email;
//            getUser.EmailConfirmed = user.EmailConfirmed;
//            getUser.LockoutEnabled = user.LockoutEnabled;
//            getUser.LockoutEnd = user.LockoutEnd;
//            getUser.MemberId = user.MemberId;
//            getUser.NormalizedEmail = user.NormalizedEmail;
//            getUser.NormalizedUserName = user.NormalizedUserName;
//            getUser.PasswordHash = user.PasswordHash;
//            getUser.PhoneNumber = user.PhoneNumber;
//            getUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
//            getUser.SecurityStamp = user.SecurityStamp;
//            getUser.TwoFactorEnabled = user.TwoFactorEnabled;
//            getUser.UserName = user.UserName;

//            _unitOfWork.Repository<AspNetUser>().Update(getUser);

//            try
//            {
//                _unitOfWork.SaveChanges();
//                return Task.CompletedTask;
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return Task.CompletedTask;
//            }
//        }

//        public Task<CustomUser> FindByIdAsync(string userId)
//        {
//            return Task.FromResult(GetCustomUserById(userId));
//        }

//        public Task<CustomUser> FindByNameAsync(string normalizedUserName)
//        {
//            var getUser = _unitOfWork.Repository<AspNetUser>().GetFirst(x => x.NormalizedUserName.Equals(normalizedUserName));

//            return Task.FromResult(DBtoVMMapping(getUser));
//        }

//        public Task<CustomUser> FindByEmailAsync(string normalizedEmail)
//        {
//            var getUser = _unitOfWork.Repository<AspNetUser>().GetFirst(x => x.NormalizedEmail.Equals(normalizedEmail));

//            return Task.FromResult(DBtoVMMapping(getUser));
//        }

//        public Task SetPasswordHashAsync(CustomUser user, string passwordHash)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            user.PasswordHash = passwordHash;
//            return Task.CompletedTask;
//        }

//        public Task<string> GetPasswordHashAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            return Task.FromResult(user.PasswordHash);
//        }

//        public Task<bool> HasPasswordAsync(CustomUser user)
//        {
//            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
//        }

//        public Task SetSecurityStampAsync(CustomUser user, string stamp)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            if (stamp == null)
//            {
//                throw new ArgumentNullException(nameof(stamp));
//            }

//            user.SecurityStamp = stamp;
//            return Task.CompletedTask;
//        }

//        public Task<string> GetSecurityStampAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            return Task.FromResult(user.SecurityStamp);
//        }

//        public Task<DateTimeOffset> GetLockoutEndDateAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            DateTimeOffset localTime2 = user.LockoutEnd != null && user.LockoutEnd != DateTimeOffset.MinValue
//                ? DateTime.SpecifyKind(user.LockoutEnd.Value.DateTime, DateTimeKind.Utc)
//                : DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

//            return Task.FromResult(localTime2);
//        }

//        public Task SetLockoutEndDateAsync(CustomUser user, DateTimeOffset lockoutEnd)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            user.LockoutEnd = lockoutEnd != null ? lockoutEnd.DateTime
//                : DateTime.Now;

//            return Task.CompletedTask;
//        }

//        public Task<bool> GetLockoutEnabledAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            return Task.FromResult(user.LockoutEnabled);
//        }

//        public Task SetLockoutEnabledAsync(CustomUser user, bool enabled)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            user.LockoutEnabled = enabled;
//            return Task.CompletedTask;
//        }

//        public Task<int> IncrementAccessFailedCountAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            user.AccessFailedCount++;
//            return Task.FromResult(user.AccessFailedCount);
//        }

//        public Task ResetAccessFailedCountAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            user.AccessFailedCount = 0;
//            return Task.CompletedTask;
//        }

//        public Task<int> GetAccessFailedCountAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            return Task.FromResult(user.AccessFailedCount);
//        }


//        public Task SetPhoneNumberAsync(CustomUser user, string phoneNumber)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            user.PhoneNumber = phoneNumber;
//            return Task.CompletedTask;
//        }

//        public Task<string> GetPhoneNumberAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            return Task.FromResult(user.PhoneNumber);
//        }

//        public Task<bool> GetPhoneNumberConfirmedAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            return Task.FromResult(user.PhoneNumberConfirmed);
//        }

//        public Task SetPhoneNumberConfirmedAsync(CustomUser user, bool confirmed)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            user.PhoneNumberConfirmed = confirmed;
//            return Task.CompletedTask;
//        }

//        public Task SetTwoFactorEnabledAsync(CustomUser user, bool enabled)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            user.TwoFactorEnabled = enabled;
//            return Task.CompletedTask;
//        }

//        public Task<bool> GetTwoFactorEnabledAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            return Task.FromResult(user.TwoFactorEnabled);
//        }

//        public Task<string> GetNormalizedEmailAsync(CustomUser user, CancellationToken cancellationToken)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            return Task.FromResult(user.NormalizedEmail);
//        }

//        public Task SetNormalizedEmailAsync(CustomUser user, string normalizedEmail, CancellationToken cancellationToken)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            user.NormalizedEmail = normalizedEmail;
//            return Task.CompletedTask;
//        }

//        public Task SetEmailAsync(CustomUser user, string email)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            user.Email = email;
//            return Task.CompletedTask;
//        }

//        public Task<string> GetEmailAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            return Task.FromResult(user.Email);
//        }

//        public Task<bool> GetEmailConfirmedAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            return Task.FromResult(user.EmailConfirmed);
//        }

//        public Task SetEmailConfirmedAsync(CustomUser user, bool confirmed)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            user.EmailConfirmed = confirmed;
//            return Task.CompletedTask;
//        }

//        public Task SetUserNameAsync(CustomUser user, string userName, CancellationToken cancellationToken)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            user.UserName = userName;
//            return Task.CompletedTask;
//        }

//        public Task<string> GetUserNameAsync(CustomUser user, CancellationToken cancellationToken)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            return Task.FromResult(user.UserName);
//        }

//        public Task SetNormalizedUserNameAsync(CustomUser user, string normalizedName, CancellationToken cancellationToken)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            user.NormalizedUserName = normalizedName;
//            return Task.CompletedTask;
//        }

//        public Task<string> GetNormalizedUserNameAsync(CustomUser user, CancellationToken cancellationToken)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            return Task.FromResult(user.NormalizedUserName);
//        }

//        public Task<string> GetUserIdAsync(CustomUser user, CancellationToken cancellationToken)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            return Task.FromResult(user.Id);
//        }


//        #endregion Users

//        #region UserLogin

//        public Task AddLoginAsync(CustomUser user, UserLoginInfo login)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            if (login == null)
//            {
//                throw new ArgumentNullException(nameof(login));
//            }

//            _unitOfWork.Repository<AspNetUserLogin>().Add(CreateUserLogin(user, login));

//            try
//            {
//                _unitOfWork.SaveChanges();
//                return Task.CompletedTask;
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return Task.CompletedTask;
//            }
//        }

//        public async Task<IList<UserLoginInfo>> GetLoginsAsync(CustomUser user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            var userLogin = await _unitOfWork.Repository<AspNetUserLogin>()
//                .FindAllAsync(x => x.UserId.Equals(user.Id));

//            return userLogin.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList();
//        }

//        public Task<CustomUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
//        {
//            var userLogin = _unitOfWork.Repository<AspNetUserLogin>()
//                .GetFirst(x => x.LoginProvider.Equals(loginProvider)
//                          && x.ProviderKey.Equals(providerKey));

//            return Task.FromResult(GetCustomUserById(userLogin.UserId));
//        }


//        public Task RemoveLoginAsync(CustomUser user, UserLoginInfo login)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            var userLogin = _unitOfWork.Repository<AspNetUserLogin>()
//                .GetFirst(x => x.UserId.Equals(user.Id)
//                && x.LoginProvider.Equals(login.LoginProvider)
//                && x.ProviderKey.Equals(login.ProviderKey));

//            _unitOfWork.Repository<AspNetUserLogin>().Remove(userLogin);

//            try
//            {
//                _unitOfWork.SaveChanges();
//                return Task.CompletedTask;
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return Task.CompletedTask;
//            }
//        }

//        public Task<CustomUser> FindAsync(UserLoginInfo login)
//        {
//            var userLogin = _unitOfWork.Repository<AspNetUserLogin>()
//                .GetFirst(x => x.LoginProvider.Equals(login.LoginProvider)
//                && x.ProviderKey.Equals(login.ProviderKey));

//            return Task.FromResult(GetCustomUserById(userLogin.UserId));
//        }

//        #endregion UserLogin


//        #region UserClaims

//        public async Task<IList<Claim>> GetClaimsAsync(CustomUser user)
//        {
//            var getUserClaims = await _unitOfWork.Repository<AspNetUserClaim>()
//                .FindAllAsync(x => x.UserId.Equals(user.Id));

//            return getUserClaims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
//        }

//        public Task AddClaimAsync(CustomUser user, Claim claim)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            if (claim == null)
//            {
//                throw new ArgumentNullException(nameof(claim));
//            }

//            var userClaims = CreateUserClaim(user, claim);
//            userClaims.Id = (int)_unitOfWork.Repository<AspNetUserClaim>().GetCount(x => x.Id) + 1;

//            _unitOfWork.Repository<AspNetUserClaim>().Add(userClaims);

//            try
//            {
//                _unitOfWork.SaveChanges();
//                return Task.CompletedTask;
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return Task.CompletedTask;
//            }
//        }

//        public async Task ReplaceClaimAsync(CustomUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            if (claim == null)
//            {
//                throw new ArgumentNullException(nameof(claim));
//            }
//            if (newClaim == null)
//            {
//                throw new ArgumentNullException(nameof(newClaim));
//            }

//            var getUserClaims = await _unitOfWork.Repository<AspNetUserClaim>()
//                .FindAllAsync(x => x.UserId.Equals(user.Id)
//                && x.ClaimType.Equals(claim.Type)
//                && x.ClaimValue.Equals(claim.Value));

//            foreach (var matchedClaim in getUserClaims)
//            {
//                matchedClaim.ClaimValue = newClaim.Value;
//                matchedClaim.ClaimType = newClaim.Type;
//            }
//        }

//        public Task RemoveClaimAsync(CustomUser user, Claim claim)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }
//            if (claim == null)
//            {
//                throw new ArgumentNullException(nameof(claim));
//            }

//            var getUserClaims = _unitOfWork.Repository<AspNetUserClaim>()
//                .FindAll(x => x.UserId.Equals(user.Id)
//                && x.ClaimType.Equals(claim.Type)
//                && x.ClaimValue.Equals(claim.Value));

//            foreach (var c in getUserClaims)
//            {
//                _unitOfWork.Repository<AspNetUserClaim>().Remove(c);
//            }

//            try
//            {
//                _unitOfWork.SaveChanges();
//                return Task.CompletedTask;
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return Task.CompletedTask;
//            }
//        }

//        public async Task<IList<CustomUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
//        {
//            if (claim == null)
//            {
//                throw new ArgumentNullException(nameof(claim));
//            }

//            var getUserClaims = await _unitOfWork.Repository<AspNetUserClaim>()
//                .FindAllAsync(x => x.ClaimType.Equals(claim.Type)
//                && x.ClaimValue.Equals(claim.Value));

//            var vm_Customer = new List<CustomUser>();

//            foreach (var item in getUserClaims)
//            {
//                var vm_user = GetCustomUserById(item.UserId);
//                vm_Customer.Add(vm_user);
//            }

//            return await Task.FromResult(vm_Customer);
//        }

//        #endregion UserClaims

//        //#region IUserRoleStore<IdentityUser, Guid> Members

//        //public Task AddToRoleAsync(CustomUser user, string roleName)
//        //{
//        //    if (user == null)
//        //        throw new ArgumentNullException("user");
//        //    if (string.IsNullOrWhiteSpace(roleName))
//        //        throw new ArgumentException("Argument cannot be null, empty, or whitespace: roleName.");

//        //    var u = _unitOfWork.Repository<AspNetUser>().GetFirst(x => x.Id.Equals(user.Id));
//        //    if (u == null)
//        //        throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
//        //    var r = _unitOfWork.Repository<AspNetRole>().GetFirst(x => x.Name.Equals(roleName));
//        //    if (r == null)
//        //        throw new ArgumentException("roleName does not correspond to a Role entity.", "roleName");

//        //    var userRole = new AspNetUserRole();
//        //    userRole.RoleId = r.Id;
//        //    userRole.UserId = u.Id;

//        //    u.AspNetUserRoles.Add(userRole);
//        //    _unitOfWork.Repository<AspNetUser>().Update(u);

//        //    try
//        //    {
//        //        _unitOfWork.SaveChanges();
//        //        return Task.CompletedTask;
//        //    }
//        //    catch (DbUpdateConcurrencyException)
//        //    {
//        //        return Task.CompletedTask;
//        //    }
//        //}

//        //public Task<IList<string>> GetRolesAsync(CustomUser user)
//        //{
//        //    if (user == null)
//        //        throw new ArgumentNullException("user");

//        //    var u = _unitOfWork.Repository<AspNetUserRoles>().GetFirst(x => x..Equals(user.Id));
//        //    if (u == null)
//        //        throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

//        //    return Task.FromResult<IList<string>>(u.AspNetUserRoles.Select(x => x.RoleId).ToList());
//        //}

//        //public Task<bool> IsInRoleAsync(CustomUser user, string roleName)
//        //{
//        //    if (user == null)
//        //        throw new ArgumentNullException("user");
//        //    if (string.IsNullOrWhiteSpace(roleName))
//        //        throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

//        //    var u = _unitOfWork.Repository<AspNetUser>().GetFirst(x => x.Id.Equals(user.Id));
//        //    if (u == null)
//        //        throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

//        //    return Task.FromResult<bool>(u.AspNetUserRoles.Any(x => x.Name == roleName));
//        //}

//        //public Task RemoveFromRoleAsync(CustomUser user, string roleName)
//        //{
//        //    if (user == null)
//        //        throw new ArgumentNullException("user");
//        //    if (string.IsNullOrWhiteSpace(roleName))
//        //        throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

//        //    var u = _unitOfWork.Repository<Customer>().GetFirst(x => x.Id == user.Id);
//        //    if (u == null)
//        //        throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

//        //    var r = u.CustomerRoles.FirstOrDefault(x => x.Name == roleName);
//        //    u.CustomerRoles.Remove(r);

//        //    _unitOfWork.Repository<Customer>().Update(u);
//        //    return _unitOfWork.SaveChangesAsync();
//        //}

//        //#endregion

//        public void SaveCustomer(CustomUser userr)
//        {
//            var user = VMtoDBMapping(userr);
//            user.Id = Guid.NewGuid().ToString();
//            user.ConcurrencyStamp = Guid.NewGuid().ToString();

//            var role = _unitOfWork.Repository<AspNetRole>().GetFirst(x => x.Name == CONSTANTS.CustomerRoleName);
//            if (role == null)
//                throw new ArgumentException("roleName does not correspond to a Role entity.", "roleName");

//            var userRole = new AspNetUserRole();
//            userRole.RoleId = role.Id;
//            userRole.UserId = user.Id;
//            user.AspNetUserRoles.Add(userRole);

//            _unitOfWork.Repository<AspNetUser>().Add(user);
//            _unitOfWork.SaveChanges();
//        }

//        #region Private Methods

//        private AspNetUserLogin CreateUserLogin(CustomUser user, UserLoginInfo login)
//        {
//            return new AspNetUserLogin
//            {
//                UserId = user.Id,
//                ProviderKey = login.ProviderKey,
//                LoginProvider = login.LoginProvider
//            };
//        }

//        private AspNetUserClaim CreateUserClaim(CustomUser user, Claim claim)
//        {
//            var userClaim = new AspNetUserClaim
//            {
//                UserId = user.Id,
//                ClaimType = claim.Type,
//                ClaimValue = claim.Value,
//            };

//            return userClaim;
//        }

//        private CustomUser GetCustomUserById(string userId)
//        {
//            var getUser = _unitOfWork.Repository<AspNetUser>().GetFirst(x => x.Id.Equals(userId));

//            return DBtoVMMapping(getUser);
//        }

//        private IEnumerable<CustomUser> DBtoVMMapping(IEnumerable<AspNetUser> items)
//        {
//            var obj_VM = _mapper.Map<IEnumerable<AspNetUser>, IEnumerable<CustomUser>>(items);
//            return obj_VM;
//        }

//        private CustomUser DBtoVMMapping(AspNetUser items)
//        {
//            var obj_VM = _mapper.Map<AspNetUser, CustomUser>(items);
//            return obj_VM;
//        }

//        private IEnumerable<AspNetUser> VMtoDBMapping(IEnumerable<CustomUser> items)
//        {
//            var obj_VM = _mapper.Map<IEnumerable<CustomUser>, IEnumerable<AspNetUser>>(items);
//            return obj_VM;
//        }

//        private AspNetUser VMtoDBMapping(CustomUser items)
//        {
//            var obj_VM = _mapper.Map<CustomUser, AspNetUser>(items);
//            return obj_VM;
//        }

//        #endregion

//    }
//}
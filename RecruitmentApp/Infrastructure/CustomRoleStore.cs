//using AutoMapper;
//using BusinessModelLayer.AddUpdateViewModel;
//using BusinessModelLayer.ViewModels;
//using DataLayer.Entities;
//using Microsoft.AspNet.Identity;
//using RepositoryLayer.UnitOfWork;
//using ServiceLayer.UnitOfService;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web;

//namespace RecruitmentApp.Infrastructure
//{
//    public class CustomRoleStore : IRoleStore<CustomRole, string>, IQueryableRoleStore<CustomRole, string>, IDisposable
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private IMapper _mapper;

//        public CustomRoleStore(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }

//        public IQueryable<CustomRole> Roles
//        {
//            get { return DBtoVMMapping(_unitOfWork.Repository<AspNetRole>().Entities).AsQueryable(); }
//        }


//        #region Role

//        public Task CreateAsync(CustomRole role)
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }

//            var roleModel = new AspNetRole();
//            roleModel.ConcurrencyStamp = role.ConcurrencyStamp;
//            roleModel.Name = role.Name;
//            roleModel.NormalizedName = role.NormalizedName;
//            roleModel.Id = Guid.NewGuid().ToString();

//            _unitOfWork.Repository<AspNetRole>().Add(roleModel);

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

//        public Task DeleteAsync(CustomRole role)
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }

//            var getRole = _unitOfWork.Repository<AspNetRole>()
//                .GetFirst(x => x.Name == role.Name);

//            _unitOfWork.Repository<AspNetRole>().Remove(getRole);

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

//        public Task UpdateAsync(CustomRole role)
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }

//            var getRole = _unitOfWork.Repository<AspNetRole>()
//                .GetFirst(x => x.Name == role.Name);

//            getRole.ConcurrencyStamp = role.ConcurrencyStamp;
//            getRole.Name = role.Name;
//            getRole.NormalizedName = role.NormalizedName;

//            _unitOfWork.Repository<AspNetRole>().Update(getRole);

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

//        public Task<CustomRole> FindByIdAsync(string roleId)
//        {
//            var role = _unitOfWork.Repository<AspNetRole>()
//                .GetFirst(x => x.Id.Equals(roleId));
//            return Task.FromResult(DBtoVMMapping(role));
//        }

//        public Task<CustomRole> FindByNameAsync(string normalizedRoleName)
//        {
//            var role = _unitOfWork.Repository<AspNetRole>()
//                .GetFirst(x => x.NormalizedName.Equals(normalizedRoleName));
//            return Task.FromResult(DBtoVMMapping(role));
//        }

//        public Task<string> GetNormalizedRoleNameAsync(CustomRole role, CancellationToken cancellationToken)
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }

//            return Task.FromResult(role.NormalizedName);
//        }

//        public Task SetNormalizedRoleNameAsync(CustomRole role, string normalizedName, CancellationToken cancellationToken)
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }

//            role.NormalizedName = normalizedName;
//            return Task.CompletedTask;
//        }

//        public Task<string> GetRoleIdAsync(CustomRole role, CancellationToken cancellationToken)
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }

//            return Task.FromResult(role.Id);
//        }

//        public Task<string> GetRoleNameAsync(CustomRole role, CancellationToken cancellationToken)
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }

//            return Task.FromResult(role.Name);
//        }


//        public Task SetRoleNameAsync(CustomRole role, string roleName, CancellationToken cancellationToken)
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }

//            role.Name = roleName;
//            return Task.CompletedTask;
//        }

//        #endregion Role

//        public void Dispose()
//        {
//        }


//        public Task AddClaimAsync(CustomRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }
//            if (claim == null)
//            {
//                throw new ArgumentNullException(nameof(claim));
//            }

//            var model = new AspNetRoleClaim();

//            model.ClaimType = claim.Type;
//            model.ClaimValue = claim.Value;
//            model.RoleId = role.Id.ToString();
//            model.Id = (int)_unitOfWork.Repository<AspNetRoleClaim>().GetCount(x => x.Id) + 1;

//            _unitOfWork.Repository<AspNetRoleClaim>().Add(model);

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


//        public async Task<IList<Claim>> GetClaimsAsync(CustomRole role, CancellationToken cancellationToken = default(CancellationToken))
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }

//            var rolesClaim = await _unitOfWork.Repository<AspNetRoleClaim>()
//                .FindAllAsync(x => x.RoleId.Equals(role.Id));

//            return rolesClaim.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
//        }


//        public async Task RemoveClaimAsync(CustomRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
//        {
//            if (role == null)
//            {
//                throw new ArgumentNullException(nameof(role));
//            }
//            if (claim == null)
//            {
//                throw new ArgumentNullException(nameof(claim));
//            }

//            var rolesClaim = await _unitOfWork.Repository<AspNetRoleClaim>()
//                .FindAllAsync(x => x.RoleId.Equals(role.Id)
//                && x.ClaimType.Equals(claim.Type) && x.ClaimValue.Equals(claim.Value));


//            foreach (var c in rolesClaim)
//            {
//                _unitOfWork.Repository<AspNetRoleClaim>().Remove(c);
//            }

//            try
//            {
//                _unitOfWork.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//            }
//        }



//        #region Private Methods

//        public IEnumerable<CustomRole> DBtoVMMapping(IEnumerable<AspNetRole> items)
//        {
//            var obj_VM = _mapper.Map<IEnumerable<AspNetRole>, IEnumerable<CustomRole>>(items);
//            return obj_VM;
//        }

//        public CustomRole DBtoVMMapping(AspNetRole items)
//        {
//            var obj_VM = _mapper.Map<AspNetRole, CustomRole>(items);
//            return obj_VM;
//        }

//        #endregion
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using ETohumProject.BLL.Dtos;
using ETohumProject.BLL.Services;
using ETohumProject.DAL.Models;

namespace ETohumProject.UI.Controllers.Api
{
    public class UsersController : ApiController
    {
        private UserManager _um;

        public UsersController()
        {
            _um = new UserManager();
        }
        // GET /api/users
        public IHttpActionResult GetUsers()
        {
            var userDtos = _um.GetAll();

            return Ok(userDtos);
        }

        // GET /api/users/1

        public IHttpActionResult GetUser(int id)
        {
            var user = _um.Get(id);
            if (user == null)
                return NotFound();
            return Ok(Mapper.Map<User, UserDto>(user));
        }

        // POST /api/users
        [HttpPost]
        public IHttpActionResult CreateUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = Mapper.Map<UserDto, User>(userDto);
            if (_um.Add(user))
            {
                userDto.Id = user.Id;

                return Created(new Uri(Request.RequestUri + "/" + user.Id), userDto);
            }
            return BadRequest();

        }


        // PUT /api/users/1
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userInDb = _um.Get(id);

            if (userInDb == null)
                return NotFound();


            var updatedUser = Mapper.Map(userDto, userInDb);

            if (_um.Update(id, updatedUser))
            {
                return Ok();
            }
            return NotFound();
        }

        //DELETE /api/users/1
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            var userInDb = _um.Get(id);

            if (userInDb == null)
                return NotFound();

            if (_um.Remove(userInDb))
            {
                return Ok();
            }
            return NotFound();

        }
    }
}

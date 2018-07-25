using ServiceStack;
using System;
using System.Collections.Generic;

namespace UserApp.ServiceModel
{
    [Route("/user", Verbs = "POST, PUT")]
    public class User : IReturn<UserResponse>
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public List<String> UserIds { get; set; }
    }

    public class UserResponse
    {
        public User Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/users", Verbs = "GET")]
    public class GetUsersRequest : IReturn<UserListResponse> {}

    public class UserListResponse
    {
        public List<User> Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/user/{userId}", Verbs = "GET")]
    public class GetUserRequest: IReturn<UserResponse>
    {
        public string UserId { get; set; }
    }

    [Route("/user/{userId*}", Verbs = "GET, DELETE")]
    public class DeleteUserRequest : IReturnVoid
    {
        public string UserId { get; set; }
    }
}

﻿using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace MS.Identity.API.Models;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{

}

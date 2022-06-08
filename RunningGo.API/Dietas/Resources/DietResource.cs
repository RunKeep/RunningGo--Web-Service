﻿using RunningGo.API.Shared.Resources;

namespace RunningGo.API.Dietas.Resources;

public class DietResource
{
    public int Id { set; get; }
    public string Description { set; get; }
    public string Specs { set; get; }
    public string Duration { set; get; }
    
    //Relationships
    public FoodResource Food { set; get; }
    
    public UserResource User { set; get; }
}
﻿@model $rootnamespace$.Models.ApplicationUser

@{
    ViewBag.Title = "Delete";
}

<h2>Delete.</h2>

<h3>Are you sure you want to delete this User?</h3>
<div>
    <h4>User.</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(model => model.UserName)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.UserName)
        </dd>
    </dl>

    @using (Html.BeginForm()) {
        @Html.AntiForgeryToken()

        <div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    }
</div>

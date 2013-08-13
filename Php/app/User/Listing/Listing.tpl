{extends file="app/Page.tpl"}
{block name='title'}User List{/block}
{block name=full}
	<div class="block gutter">
		<h1>Users</h1>
	</div>
	<div class="block gutter">

		<ul class="users simple list">
			{foreach $model->userModels as $userModel}
				<li>
					{partial view='app\User\Listing\Item' model=$userModel}
				</li>
			{/foreach}
		</ul>
	</div>
{/block}
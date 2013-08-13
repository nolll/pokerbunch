{extends file="app/Page.tpl"}
{block name='title'}Change Password{/block}
{block name=full}
	<div class="block gutter">
		<h1>Change Password</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Site\Errors' model=$model->validationErrors}
		<form method="post" autocomplete="off">
			<fieldset>
				<label>New Password</label>
				<p>
					<input type="password" name="password" />
				</p>
				<label>Repeat New Password</label>
				<p>
					<input type="password" name="repeat" />
				</p>
			</fieldset>
			<div class="buttons">
				<button class="action" type="submit">Save</button>
			</div>
		</form>
	</div>
{/block}
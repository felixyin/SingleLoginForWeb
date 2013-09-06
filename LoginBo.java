package com.app.pojo.bo;

import com.app.pojo.po.SysUsersEntity;

/**
 * @author yinbin
 *
 *单点登录的user对象
 */
public class LoginBo {

	private SysUsersEntity sysUsersEntity;
	/**
	 * 服务器端单点登录，传送过来的数据
	 */
	private String[] params;

	public SysUsersEntity getSysUsersEntity() {
		return sysUsersEntity;
	}

	public void setSysUsersEntity(SysUsersEntity sysUsersEntity) {
		this.sysUsersEntity = sysUsersEntity;
	}

	public String[] getParams() {
		return params;
	}

	public void setParams(String[] params) {
		this.params = params;
	}
}

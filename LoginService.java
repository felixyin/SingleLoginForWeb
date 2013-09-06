package com.app.platform.main.auth.login;

import com.app.dao.LoginDao;
import com.app.pojo.bo.LoginBo;
import com.app.pojo.json.JsonTreeNode;
import com.app.pojo.po.SysUsersEntity;
import com.frame.utils.StringUtil;
import com.opensymphony.xwork2.ActionContext;
import org.apache.commons.lang.StringUtils;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.webflow.util.RandomGuidUidGenerator;

import javax.annotation.Resource;
import java.util.*;

/**
 * Created by IntelliJ IDEA. UserEntity: Administrator Date: 2011-3-4 Time:
 * 0:02:09 Explain:
 */

@Transactional
@Service
public class LoginService {

	@Resource
	private LoginDao orgmodelUserDao;

	public LoginDao getOrgmodelUserDao() {
		return orgmodelUserDao;
	}
 
	/**
	 * 给用户注册单点登录，传入一些参数
	 * 
	 * @param paramStr
	 * @return
	 * @throws Exception
	 */
	@Transactional(propagation = Propagation.NEVER, readOnly = true)
	public String registerSingleSignon(String paramStr) throws Exception {
		/**
		 * 1.pdf签章列表 2.pdf签章页面 3.印章申请 4.印章激活 5.pdf撤签申请 6.pdf撤签审批 7.pdf撤签页面
		 * 8.pdf撤签历史
		 */
		String key = null;
		try {
			// 放入单点登录的参数
			String[] params = paramStr.split(",");
			if (null == params || 3 > params.length) {
				return null;
			}
			String accountid = params[0];
			String password = params[1];
			SysUsersEntity user = orgmodelUserDao
					.getUserEntityByAccountId(accountid);

			// 判断用户密码是否相同
			if (user.getCpassword().equalsIgnoreCase(password)) {

				LoginBo loginBo = new LoginBo();
				loginBo.setSysUsersEntity(user);
				loginBo.setParams(params);

				Map application = ActionContext.getContext().getApplication();

				// 为单点登录用户查询出角色权限信息
				// List<String> userRoles = orgmodelUserDao.getUserRoles(user
				// .getCusercode());

				// 随机生成key
				RandomGuidUidGenerator randomGuidUidGenerator = new RandomGuidUidGenerator();
				key = randomGuidUidGenerator.generateUid().toString();
				System.out.println("注册***********************************>"
						+ key);

				// Map<String, String> map = (Map<String, String>)
				// application.get("timestampMap");
				// if (map == null) {
				// Map<String, String> map2 = new HashMap<String, String>();
				// application.put("timestampMap", map2);
				// }

				// 将用户登录信息 关联 应用上下文
				application.put(key, loginBo);

				// 给key记上 时间戳
				// map.put("date", key);

				// 添加到在线用户列表中 ，并初始化此用户的消息容器 TODO
				// this.loginForMsgModel(user, ActionContext.getContext()
				// .getApplication());
				return key;
			}

		} catch (Exception e) {
			e.printStackTrace();
			return null;
		}
		return key;
	}

	/**
	 * 真正的单点登录方法,根据key进行登录
	 * 
	 * @param key
	 * @return
	 * @throws Exception
	 */
	public LoginBo singleSignon(String key) throws Exception {
		LoginBo loginBo = null;
		try {
			ActionContext actionContext = ActionContext.getContext();

			// 使用钥匙，获取应用上下文中 存储 的用户信息
			Map application = actionContext.getApplication();
			Object obj = application.get(key);

			// 关联 用户信息到 当前登录用户的 session中
			Map map = actionContext.getSession();
			loginBo = (LoginBo) obj;
			map.put("user", loginBo.getSysUsersEntity());

			application.remove(key);// 删除 应用上下文中的 用户信息
			System.out.println("清理***********************************>" + key);
		} catch (Exception e) {
			return null;
		}
		return loginBo;
	}
}

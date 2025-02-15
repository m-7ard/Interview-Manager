import { Header, SkipToContent, HeaderMenuButton, HeaderName, HeaderGlobalBar, HeaderGlobalAction, SideNav, SideNavItems, SideNavMenu, SideNavMenuItem, SideNavLink } from "@carbon/react";
import { Notification, UserAvatar, Switcher, Fade } from "@carbon/icons-react";
import { useState } from "react";

export default function AppHeader() {
    const [isSideNavExpanded, setIsSideNavExpanded] = useState(false);

    const handleClickSideNavExpand = () => {
        setIsSideNavExpanded(!isSideNavExpanded);
    };

    return (
        <Header aria-label="Carbon Tutorial">
            <SkipToContent />
            <HeaderMenuButton aria-label="Open menu" isCollapsible onClick={handleClickSideNavExpand} isActive={isSideNavExpanded} />

            <HeaderName href="/" prefix="IBM">
                Cloud PAK Automation Hub
            </HeaderName>

            <HeaderGlobalBar>
                <HeaderGlobalAction aria-label="Notifications">
                    <Notification size={20} />
                </HeaderGlobalAction>
                <HeaderGlobalAction aria-label="User Avatar">
                    <UserAvatar size={20} />
                </HeaderGlobalAction>
                <HeaderGlobalAction aria-label="App Switcher">
                    <Switcher size={20} />
                </HeaderGlobalAction>
            </HeaderGlobalBar>

            {/* ----------------------------- */}
            <SideNav aria-label="Side navigation" isRail expanded={isSideNavExpanded}>
                <SideNavItems>
                    <SideNavMenu renderIcon={Fade} title="Category title">
                        <SideNavMenuItem href="javascript:void(0)">Link</SideNavMenuItem>
                        <SideNavMenuItem aria-current="page" href="javascript:void(0)">
                            Link
                        </SideNavMenuItem>
                        <SideNavMenuItem href="javascript:void(0)">Link</SideNavMenuItem>
                    </SideNavMenu>
                    <SideNavMenu renderIcon={Fade} title="Category title">
                        <SideNavMenuItem href="javascript:void(0)">Link</SideNavMenuItem>
                        <SideNavMenuItem aria-current="page" href="javascript:void(0)">
                            Link
                        </SideNavMenuItem>
                        <SideNavMenuItem href="javascript:void(0)">Link</SideNavMenuItem>
                    </SideNavMenu>
                    <SideNavMenu renderIcon={Fade} title="Category title">
                        <SideNavMenuItem href="javascript:void(0)">Link</SideNavMenuItem>
                        <SideNavMenuItem aria-current="page" href="javascript:void(0)">
                            Link
                        </SideNavMenuItem>
                        <SideNavMenuItem href="javascript:void(0)">Link</SideNavMenuItem>
                    </SideNavMenu>
                    <SideNavLink renderIcon={Fade} href="javascript:void(0)">
                        Link
                    </SideNavLink>
                    <SideNavLink renderIcon={Fade} href="javascript:void(0)">
                        Link
                    </SideNavLink>
                </SideNavItems>
            </SideNav>
            {/* ----------------------------- */}
        </Header>
    )
}
import { WebsiteLayout } from "presentation/layouts/WebsiteLayout";
import { Fragment, memo } from "react";
import { Box } from "@mui/system";
import { Seo } from "@presentation/components/ui/Seo";
import { ContentCard } from "@presentation/components/ui/ContentCard";
import { UserTable } from "@presentation/components/ui/Tables/UserTable";
import { TagsTable } from "@presentation/components/ui/Tables/TagsTable";

export const ProductTagsPage = memo(() => {
  return <Fragment>
    <Seo title="MobyLab Web App | Users" />
    <WebsiteLayout>
      <Box sx={{ padding: "0px 50px 00px 50px", justifyItems: "center" }}>
        <ContentCard>
          <TagsTable/>
        </ContentCard>
      </Box>
    </WebsiteLayout>
  </Fragment>
});

AzCopy /Source:  /Dest:  /SourceKey:8CjlThv9HZxU5lgKSaYDrTuudMHz1YUk55beoKFYiaSoW2FJO/0UPtxfIwrhaf6BRyT6I7RQXIUVcHmTzheXdg== /DestKey: /Pattern:Sample.txt

# Download from Storage Blob Container to local hard drive (only one file)
AzCopy /Source:https://whizlabsstore.blob.core.windows.net/input  /Dest:D:whizlabs  /SourceKey:8CjlThv9HZxU5lgKSaYDrTuudMHz1YUk55beoKFYiaSoW2FJO/0UPtxfIwrhaf6BRyT6I7RQXIUVcHmTzheXdg== /Pattern:Sample.txt

# Download ALL files from Storage Blob Container to local hard drive
AzCopy /Source:https://whizlabsstore.blob.core.windows.net/input  /Dest:D:whizlabs  /SourceKey:8CjlThv9HZxU5lgKSaYDrTuudMHz1YUk55beoKFYiaSoW2FJO/0UPtxfIwrhaf6BRyT6I7RQXIUVcHmTzheXdg== /S

# Copy file from local hard drive to blob container
AzCopy /Dest:https://whizlabsstore.blob.core.windows.net/output  /Source:D:whizlabs  /DestKey:8CjlThv9HZxU5lgKSaYDrTuudMHz1YUk55beoKFYiaSoW2FJO/0UPtxfIwrhaf6BRyT6I7RQXIUVcHmTzheXdg== /Pattern:Sample.txt

# Copy file from blob container to blob container in same storage account
AzCopy /Source:https://whizlabsstore.blob.core.windows.net/input /Dest:https://whizlabsstore.blob.core.windows.net/output /SourceKey:8CjlThv9HZxU5lgKSaYDrTuudMHz1YUk55beoKFYiaSoW2FJO/0UPtxfIwrhaf6BRyT6I7RQXIUVcHmTzheXdg==  /DestKey:8CjlThv9HZxU5lgKSaYDrTuudMHz1YUk55beoKFYiaSoW2FJO/0UPtxfIwrhaf6BRyT6I7RQXIUVcHmTzheXdg== /Pattern:Sample.txt

# Copy file from blob container to blob container in same storage account
AzCopy /Source:https://whizlabsstore.blob.core.windows.net/input /Dest:https://whizlabslogs.blob.core.windows.net/output /SourceKey:8CjlThv9HZxU5lgKSaYDrTuudMHz1YUk55beoKFYiaSoW2FJO/0UPtxfIwrhaf6BRyT6I7RQXIUVcHmTzheXdg==  /DestKey:XaJ32TbBCvVIcdkL91yEkqefT3tWU71xfXqc4WtN2AEjTYNNspbLcTAxqwPSiv2sOW3N2MxcQ0X1s4g6w5qkYQ== /Pattern:Sample.txt

#NOTE: if container is not created then azcopy will create it and copy the files into it.
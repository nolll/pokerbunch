import api from '@/api';
import { MessageResponse } from '@/response/MessageResponse';
import { useMutation } from '@tanstack/vue-query';

const sendTestEmail = async (): Promise<MessageResponse> => {
  var response = await api.sendEmail();
  return response.data;
};

export const useSendTestEmailMutation = (onComplete?: (response: MessageResponse) => void) => {
  return useMutation({
    mutationFn: sendTestEmail,
    onSuccess: onComplete,
  });
};
